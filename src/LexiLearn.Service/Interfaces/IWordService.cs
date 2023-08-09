using LexiLearn.Domain.Entities.Words;
using LexiLearn.Domain.Enums;
using LexiLearn.Service.DTOs.Words;
using LexiLearn.Service.Helpers;

namespace LexiLearn.Service.Interfaces;

public interface IWordService
{
    Task<Response<WordResultDto>> CreateAsync(WordCreationDto dto);

    Task<Response<WordResultDto>> UpdateAsync(WordUpdateDto dto);

    Task<Response<bool>> DeleteAsync(long id);

    Task<Response<Word>> GetByIdAsync(long id);

    Task<Response<IEnumerable<Word>>> GetAllAsync();

    Task<Response<IEnumerable<Word>>> GetWordsByCategoryAsync(long categoryId);

    Task<Response<IEnumerable<Word>>> SearchWordsAsync(string searchTerm);
}