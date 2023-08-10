using AutoMapper;
using LexiLearn.DAL.IRepositories;
using LexiLearn.DAL.Repository;
using LexiLearn.Domain.Entities.Questions;
using LexiLearn.Domain.Entities.Words;
using LexiLearn.Domain.Enums;
using LexiLearn.Service.DTOs.Questions;
using LexiLearn.Service.Helpers;
using LexiLearn.Service.Interfaces;
using LexiLearn.Service.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LexiLearn.Service.Services;

public class QuestionService : IQuestionService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public QuestionService()
    {
        unitOfWork = new UnitOfWork();
        mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        }));
    }

    public async Task<Response<QuestionResultDto>> CreateAsync(QuestionCreationDto dto)
    {
        if (!await IsWordValid(dto.WordId))
        {
            return new Response<QuestionResultDto>
            {
                StatusCode = 400,
                Message = "Invalid word ID",
                Data = null
            };
        }

        var newQuestion = mapper.Map<Question>(dto);
        unitOfWork.QuestionRepository.Add(newQuestion);
        unitOfWork.QuestionRepository.SaveChanges();

        var resultDto = mapper.Map<QuestionResultDto>(newQuestion);

        return new Response<QuestionResultDto>
        {
            StatusCode = 201,
            Message = "Question created",
            Data = resultDto
        };
    }

    public async Task<Response<QuestionResultDto>> UpdateAsync(QuestionUpdateDto dto)
    {
        var existingQuestion = unitOfWork.QuestionRepository.Select(dto.Id);

        if (existingQuestion is null)
        {
            return new Response<QuestionResultDto>
            {
                StatusCode = 404,
                Message = "Question not found",
                Data = null
            };
        }

        if (!await IsWordValid(dto.WordId))
        {
            return new Response<QuestionResultDto>
            {
                StatusCode = 400,
                Message = "Invalid word ID",
                Data = null
            };
        }

        mapper.Map(dto, existingQuestion);
        unitOfWork.QuestionRepository.Update(existingQuestion);
        unitOfWork.QuestionRepository.SaveChanges();

        var resultDto = mapper.Map<QuestionResultDto>(existingQuestion);

        return new Response<QuestionResultDto>
        {
            StatusCode = 200,
            Message = "Question updated",
            Data = resultDto
        };
    }

    public async Task<Response<bool>> DeleteAsync(long id)
    {
        var existingQuestion = unitOfWork.QuestionRepository.Select(id);
        if (existingQuestion is null)
        {
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "Question not found",
                Data = false
            };
        }

        unitOfWork.QuestionRepository.Delete(existingQuestion);
        unitOfWork.QuestionRepository.SaveChanges();

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Question deleted",
            Data = true
        };
    }

    public async Task<Response<Question>> GetByIdAsync(long id)
    {
        var question = unitOfWork.QuestionRepository.Select(id);

        if (question is null)
        {
            return new Response<Question>
            {
                StatusCode = 404,
                Message = "Question not found",
                Data = null
            };
        }

        return new Response<Question>
        {
            StatusCode = 200,
            Message = "Question returned",
            Data = question
        };
    }

    public async Task<Response<IEnumerable<Question>>> GetAllAsync()
    {
        var allQuestions = unitOfWork.QuestionRepository.SelectAll();

        if (allQuestions is null)
        {
            return new Response<IEnumerable<Question>>
            {
                StatusCode = 404,
                Message = "No questions found",
                Data = null
            };
        }

        return new Response<IEnumerable<Question>>
        {
            StatusCode = 200,
            Message = "All questions returned",
            Data = allQuestions
        };
    }

    public async Task<Response<IEnumerable<Question>>> GetQuestionsByWordAsync(long wordId)
    {
        var questionsForWord = unitOfWork.QuestionRepository.SelectAll()
            .Where(q => q.WordId == wordId).Include(w => w.Word);

        if (questionsForWord is null)
        {
            return new Response<IEnumerable<Question>>
            {
                StatusCode = 404,
                Message = "No questions found for the specified word",
                Data = null
            };
        }

        return new Response<IEnumerable<Question>>
        {
            StatusCode = 200,
            Message = "Questions for the specified word returned",
            Data = questionsForWord
        };
    }

    public async Task<Response<IEnumerable<Question>>> GetQuestionsByTypeAsync(QuestionType type)
    {
        var questionsByType = unitOfWork.QuestionRepository.SelectAll()
            .Where(q => q.Type == type).Include(q => q.Word);

        if (questionsByType is null)
        {
            return new Response<IEnumerable<Question>>
            {
                StatusCode = 404,
                Message = "No questions found for the specified type",
                Data = null
            };
        }

        return new Response<IEnumerable<Question>>
        {
            StatusCode = 200,
            Message = "Questions for the specified type returned",
            Data = questionsByType
        };
    }

    private async Task<bool> IsWordValid(long wordId)
    {
        var word = unitOfWork.WordRepository.Select(wordId);
        return word != null;
    }

    public async Task PrintQuestionAsync(long id)
    {
        var question = unitOfWork.QuestionRepository.SelectAll().Include(q => q.Word)
            .FirstOrDefault(q => q.Id == id);

        if (question is null)
            return;

        if (question.Type == QuestionType.EngToUzb)
            Console.Write($"Translate {question.Word.Text} to Uzbek language: ");
        else
            Console.Write($"{question.Word.Text} so'zini ingliz tiliga tarjima qiling: ");
    }

    public async Task<Response<bool>> CheckAsync(long id, string answer)
    {
        var question = unitOfWork.QuestionRepository.SelectAll().Include(q => q.Word)
            .FirstOrDefault(q => q.Id == id);

        if(question is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "Question not found",
                Data = false
            };

        if(answer.Trim().ToLower() == question.Word.Translation)
            return new Response<bool>
            {
                StatusCode = 200,
                Message = "Answer is true",
                Data = true
            };
        
        return new Response<bool>
        {
            StatusCode = 400,
            Message = "Answer is wrong",
            Data = false
        };
    }

    public async Task<Response<IEnumerable<Question>>> GetQuestionsByQuizIdAsync(long quizId)
    {
        var questionsForQuiz = unitOfWork.QuestionRepository.SelectAll()
            .Where(q => q.QuizId == quizId).Include(w => w.Quiz);

        if (questionsForQuiz is null)
        {
            return new Response<IEnumerable<Question>>
            {
                StatusCode = 404,
                Message = "No questions found for the specified word",
                Data = null
            };
        }

        return new Response<IEnumerable<Question>>
        {
            StatusCode = 200,
            Message = "Questions for the specified word returned",
            Data = questionsForQuiz
        };
    }
}
