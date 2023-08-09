using LexiLearn.Domain.Entities.Quizzes;
using LexiLearn.Service.DTOs.Quizzes.QuizHistory;
using LexiLearn.Service.Helpers;

namespace LexiLearn.Service.Interfaces;

public interface IQuizHistoryService
{
    Task<Response<QuizHistoryResultDto>> CreateAsync(QuizHistoryCreationDto dto);

    Task<Response<IEnumerable<QuizHistory>>> GetQuizHistoryByUserIdAsync(long userId);

    Task<Response<IEnumerable<QuizHistory>>> GetQuizHistoryByQuizIdAsync(long quizId);

    Task<Response<bool>> DeleteAsync(long id);

    Task<Response<QuizHistory>> GetByIdAsync(long id);

    Task<Response<bool>> IsValidId(long userId, long quizId);
}