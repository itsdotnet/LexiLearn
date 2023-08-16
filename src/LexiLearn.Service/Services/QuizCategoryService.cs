using AutoMapper;
using LexiLearn.DAL.Constexts;
using LexiLearn.DAL.IRepositories;
using LexiLearn.DAL.Repository;
using LexiLearn.Domain.Entities.Categories;
using LexiLearn.Service.DTOs.Categories;
using LexiLearn.Service.Helpers;
using LexiLearn.Service.Interfaces;
using LexiLearn.Service.Mappers;

namespace LexiLearn.Service.Services;

public class QuizCategoryService : IQuizCategoryService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public QuizCategoryService(LexiLearnDbContext dbContext)
    {
        unitOfWork = new UnitOfWork(dbContext);
        mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        }));
    }

    public async Task<Response<CategoryResultDto>> CreateAsync(CategoryCreationDto dto)
    {
        var newCategory = mapper.Map<QuizCategory>(dto);
        unitOfWork.QuizCategoryRepository.Add(newCategory);
        unitOfWork.QuizCategoryRepository.SaveChanges();

        var resultDto = mapper.Map<CategoryResultDto>(newCategory);

        return new Response<CategoryResultDto>
        {
            StatusCode = 201,
            Message = "Quiz category created",
            Data = resultDto
        };
    }

    public async Task<Response<CategoryResultDto>> UpdateAsync(CategoryUpdateDto dto)
    {
        var existingCategory = unitOfWork.QuizCategoryRepository.Select(dto.Id);
        if (existingCategory is null)
        {
            return new Response<CategoryResultDto>
            {
                StatusCode = 404,
                Message = "Quiz category not found",
                Data = null
            };
        }

        mapper.Map(dto, existingCategory);
        unitOfWork.QuizCategoryRepository.Update(existingCategory);
        unitOfWork.QuizCategoryRepository.SaveChanges();

        var resultDto = mapper.Map<CategoryResultDto>(existingCategory);

        return new Response<CategoryResultDto>
        {
            StatusCode = 200,
            Message = "Quiz category updated",
            Data = resultDto
        };
    }

    public async Task<Response<bool>> DeleteAsync(long id)
    {
        var existingCategory = unitOfWork.QuizCategoryRepository.Select(id);
        if (existingCategory is null)
        {
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "Quiz category not found",
                Data = false
            };
        }

        unitOfWork.QuizCategoryRepository.Delete(existingCategory);
        unitOfWork.QuizCategoryRepository.SaveChanges();

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Quiz category deleted",
            Data = true
        };
    }

    public async Task<Response<QuizCategory>> GetByIdAsync(long id)
    {
        var category = unitOfWork.QuizCategoryRepository.Select(id);

        if (category is null)
        {
            return new Response<QuizCategory>
            {
                StatusCode = 404,
                Message = "Quiz category not found",
                Data = null
            };
        }

        return new Response<QuizCategory>
        {
            StatusCode = 200,
            Message = "Quiz category returned",
            Data = category
        };
    }

    public async Task<Response<IEnumerable<QuizCategory>>> GetAllAsync()
    {
        var allCategories = unitOfWork.QuizCategoryRepository.SelectAll();

        if (allCategories is null)
        {
            return new Response<IEnumerable<QuizCategory>>
            {
                StatusCode = 404,
                Message = "No quiz categories found",
                Data = null
            };
        }

        return new Response<IEnumerable<QuizCategory>>
        {
            StatusCode = 200,
            Message = "All quiz categories returned",
            Data = allCategories
        };
    }
}
