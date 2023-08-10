using AutoMapper;
using LexiLearn.DAL.IRepositories;
using LexiLearn.DAL.Repository;
using LexiLearn.Domain.Entities.Quizzes;
using LexiLearn.Domain.Enums;
using LexiLearn.Service.DTOs.Quizzes;
using LexiLearn.Service.Helpers;
using LexiLearn.Service.Interfaces;
using LexiLearn.Service.Mappers;

namespace LexiLearn.Service.Services;

public class QuizService : IQuizService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public QuizService()
    {
        this.unitOfWork = new UnitOfWork();
        this.mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        }));
    }

    public async Task<Response<QuizResultDto>> CreateAsync(QuizCreationDto dto)
    {
        if(!(await IsValidCategoryId(dto.CategoryId)).Data)
            return new Response<QuizResultDto>()
            {
                StatusCode = 400,
                Message = "Category ID invalid",
                Data = null
            };

        var newQuiz = mapper.Map<Quiz>(dto);

        unitOfWork.QuizRepository.Add(newQuiz);
        unitOfWork.QuizRepository.SaveChanges();

        var resultDto = mapper.Map<QuizResultDto>(newQuiz);

        return new Response<QuizResultDto>
        {
            StatusCode = 200,
            Message = "Quiz created",
            Data = resultDto
        };
    }

    public async Task<Response<QuizResultDto>> UpdateAsync(QuizUpdateDto dto)
    {
        if ((await IsValidCategoryId(dto.CategoryId)).Data)
            return new Response<QuizResultDto>()
            {
                StatusCode = 400,
                Message = "Category ID invalid",
                Data = null
            };

        var existingQuiz = unitOfWork.QuizRepository.Select(dto.Id);

        if (existingQuiz is null)
        {
            return new Response<QuizResultDto>
            {
                StatusCode = 404,
                Message = "Quiz not found",
                Data = null
            };
        }

        mapper.Map(dto, existingQuiz);
        unitOfWork.QuizRepository.Update(existingQuiz);
        unitOfWork.QuizRepository.SaveChanges();

        var resultDto = mapper.Map<QuizResultDto>(existingQuiz);

        return new Response<QuizResultDto>
        {
            StatusCode = 200,
            Message = "Quiz updated",
            Data = resultDto
        };
    }

    public async Task<Response<bool>> DeleteAsync(long id)
    {
        var existingQuiz = unitOfWork.QuizRepository.Select(id);

        if (existingQuiz is null)
        {
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "Quiz not found",
                Data = false
            };
        }

        unitOfWork.QuizRepository.Delete(existingQuiz);
        unitOfWork.QuizRepository.SaveChanges();

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Quiz deleted",
            Data = true
        };
    }

    public async Task<Response<Quiz>> GetByIdAsync(long id)
    {
        var quiz = unitOfWork.QuizRepository.Select(id);

        if (quiz is null)
        {
            return new Response<Quiz>
            {
                StatusCode = 404,
                Message = "Quiz not found",
                Data = null
            };
        }

        return new Response<Quiz>
        {
            StatusCode = 200,
            Message = "Quiz returned",
            Data = quiz
        };
    }

    public async Task<Response<IEnumerable<Quiz>>> GetAllAsync()
    {
        var allQuizzes = unitOfWork.QuizRepository.SelectAll();

        if (allQuizzes is null)
        {
            return new Response<IEnumerable<Quiz>>
            {
                StatusCode = 404,
                Message = "No quizzes found",
                Data = null
            };
        }

        return new Response<IEnumerable<Quiz>>
        {
            StatusCode = 200,
            Message = "All quizzes returned",
            Data = allQuizzes
        };
    }

    public async Task<Response<IEnumerable<Quiz>>> GetQuizzesByCategoryAsync(long categoryId)
    {
        var quizzesInCategory = unitOfWork.QuizRepository.SelectAll()
            .Where(q => q.CategoryId == categoryId);

        if (quizzesInCategory is null)
        {
            return new Response<IEnumerable<Quiz>>
            {
                StatusCode = 404,
                Message = "No quizzes found in the specified category",
                Data = null
            };
        }

        return new Response<IEnumerable<Quiz>>
        {
            StatusCode = 200,
            Message = "Quizzes in the specified category returned",
            Data = quizzesInCategory
        };
    }

    public async Task<Response<IEnumerable<Quiz>>> SearchQuizzesAsync(string searchTerm)
    {
        var searchedQuizzes = unitOfWork.QuizRepository.SelectAll()
            .Where(q => q.Title.StartsWith(searchTerm.ToLower().Trim()));

        if (searchedQuizzes is null)
        {
            return new Response<IEnumerable<Quiz>>
            {
                StatusCode = 404,
                Message = "No quizzes found for the specified search term",
                Data = null
            };
        }

        return new Response<IEnumerable<Quiz>>
        {
            StatusCode = 200,
            Message = "Quizzes matching the search term returned",
            Data = searchedQuizzes
        };
    }

    public async Task<Response<IEnumerable<Quiz>>> GetQuizzesByLevelAsync(QuizLevel level)
    {
        var quizzesByLevel = unitOfWork.QuizRepository.SelectAll()
            .Where(q => q.Level == level);

        if (quizzesByLevel is null)
        {
            return new Response<IEnumerable<Quiz>>
            {
                StatusCode = 404,
                Message = "No quizzes found for the specified level",
                Data = null
            };
        }

        return new Response<IEnumerable<Quiz>>
        {
            StatusCode = 200,
            Message = "Quizzes for the specified level returned",
            Data = quizzesByLevel
        };
    }

    public async Task<Response<bool>> IsValidCategoryId(long categoryId)
    {
        var category = unitOfWork.QuizCategoryRepository.Select(categoryId);

        if (category is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "Category not found",
                Data = false
            };

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Category is exsist",
            Data = true
        };
    }
}