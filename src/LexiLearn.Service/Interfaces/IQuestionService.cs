using LexiLearn.Domain.Entities.Questions;
using LexiLearn.Service.DTOs.Questions;
using LexiLearn.Service.Helpers;

namespace LexiLearn.Service.Interfaces;

public interface IQuestionService
{
    Task<Response<QuestionResultDto>> CreateAsync(QuestionCreationDto dto);

    Task<Response<QuestionResultDto>> UpdateAsync(QuestionUpdateDto dto);

    Task PrintQuestionAsync(long id);

    Task<Response<bool>> DeleteAsync(long id);

    Task<Response<Question>> GetByIdAsync(long id);

    Task<Response<IEnumerable<Question>>> GetAllAsync();

    Task<Response<IEnumerable<Question>>> GetQuestionsByWordAsync(long wordId);

    Task<Response<bool>> CheckAsync(long id, string answer);
}
