using AutoMapper;
using LexiLearn.DAL.IRepositories;
using LexiLearn.DAL.Repository;
using LexiLearn.Domain.Entities.Quizzes;
using LexiLearn.Service.DTOs.Quizzes.QuizHistory;
using LexiLearn.Service.Helpers;
using LexiLearn.Service.Interfaces;
using LexiLearn.Service.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LexiLearn.Service.Services;

public class QuizHistoryService : IQuizHistoryService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public QuizHistoryService()
    {
        unitOfWork = new UnitOfWork();
        mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        }));
    }

    public async Task<Response<QuizHistoryResultDto>> CreateAsync(QuizHistoryCreationDto dto)
    {
        var response = await IsValidId(dto.UserId, dto.QuizId);
        if (response.Data)
            return new Response<QuizHistoryResultDto>
            {
                StatusCode = 200,
                Message = "User ID or Quiz ID invalid",
                Data = null
            };

        var newQuizHistory = mapper.Map<QuizHistory>(dto);

        unitOfWork.QuizHistoryRepository.Add(newQuizHistory);
        unitOfWork.QuizHistoryRepository.SaveChanges();

        var resultDto = mapper.Map<QuizHistoryResultDto>(newQuizHistory);

        return new Response<QuizHistoryResultDto>
        {
            StatusCode = 200,
            Message = "Quiz history created",
            Data = resultDto
        };
    }

    public async Task<Response<IEnumerable<QuizHistory>>> GetQuizHistoryByUserIdAsync(long userId)
    {
        var quizHistoryByUserId = unitOfWork.QuizHistoryRepository.SelectAll()
            .Include(u => u.Quiz)
                .Where(qh => qh.UserId == userId);

        return new Response<IEnumerable<QuizHistory>>
        {
            StatusCode = 200,
            Message = "Quiz history for the specified user returned",
            Data = quizHistoryByUserId
        };
    }

    public async Task<Response<IEnumerable<QuizHistory>>> GetQuizHistoryByQuizIdAsync(long quizId)
    {
        var quizHistoryByQuizId = unitOfWork.QuizHistoryRepository.SelectAll()
            .Where(qh => qh.QuizId == quizId);

        return new Response<IEnumerable<QuizHistory>>
        {
            StatusCode = 200,
            Message = "Quiz history for the specified quiz returned",
            Data = quizHistoryByQuizId
        };
    }

    public async Task<Response<bool>> DeleteAsync(long id)
    {
        var existingQuizHistory = unitOfWork.QuizHistoryRepository.Select(id);

        if (existingQuizHistory is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "Quiz history not found",
                Data = false
            };

        unitOfWork.QuizHistoryRepository.Delete(existingQuizHistory);
        unitOfWork.QuizHistoryRepository.SaveChanges();

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Quiz history deleted",
            Data = true
        };
    }

    public async Task<Response<QuizHistory>> GetByIdAsync(long id)
    {
        var quizHistory = unitOfWork.QuizHistoryRepository.Select(id);

        if (quizHistory is null)
            return new Response<QuizHistory>
            {
                StatusCode = 404,
                Message = "Quiz history not found",
                Data = null
            };

        return new Response<QuizHistory>
        {
            StatusCode = 200,
            Message = "Quiz history returned",
            Data = quizHistory
        };
    }

    public async Task<Response<bool>> IsValidId(long userId, long quizId)
    {
        var user = unitOfWork.UserRepository.Select(userId);
        var quiz = unitOfWork.QuizRepository.Select(quizId);

        if (user is null || quiz is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "Quiz or User not found",
                Data = false
            };

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Quiz and User exsist",
            Data = true
        };
    }
}
