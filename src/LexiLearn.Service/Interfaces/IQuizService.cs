using LexiLearn.Domain.Entities.Quizzes;
using LexiLearn.Domain.Enums;
using LexiLearn.Service.DTOs.Quizzes;
using LexiLearn.Service.Helpers;

namespace LexiLearn.Service.Interfaces;

public interface IQuizService
{
    Task<Response<QuizResultDto>> CreateAsync(QuizCreationDto dto);

    Task<Response<QuizResultDto>> UpdateAsync(QuizUpdateDto dto);

    Task<Response<bool>> DeleteAsync(long id);

    Task<Response<Quiz>> GetByIdAsync(long id);

    Task<Response<IEnumerable<Quiz>>> GetAllAsync();

    Task<Response<IEnumerable<Quiz>>> GetQuizzesByCategoryAsync(long categoryId);

    Task<Response<IEnumerable<Quiz>>> SearchQuizzesAsync(string searchTerm);

    Task<Response<IEnumerable<Quiz>>> GetQuizzesByLevelAsync(QuizLevel level);
}
