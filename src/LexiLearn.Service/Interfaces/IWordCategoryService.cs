using LexiLearn.Domain.Entities.Categories;
using LexiLearn.Service.DTOs.Categories;
using LexiLearn.Service.Helpers;

namespace LexiLearn.Service.Interfaces;

public interface IWordCategoryService
{
    Task<Response<CategoryResultDto>> CreateAsync(CategoryCreationDto dto);

    Task<Response<CategoryResultDto>> UpdateAsync(CategoryUpdateDto dto);

    Task<Response<bool>> DeleteAsync(long id);

    Task<Response<WordCategory>> GetByIdAsync(long id);

    Task<Response<IEnumerable<WordCategory>>> GetAllAsync();
}