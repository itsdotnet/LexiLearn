using AutoMapper;
using LexiLearn.DAL.IRepositories;
using LexiLearn.DAL.Repository;
using LexiLearn.Domain.Entities.Categories;
using LexiLearn.Service.DTOs.Categories;
using LexiLearn.Service.Helpers;
using LexiLearn.Service.Interfaces;
using LexiLearn.Service.Mappers;

namespace LexiLearn.Service.Services
{
    public class WordCategoryService : IWordCategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public WordCategoryService()
        {
            unitOfWork = new UnitOfWork();
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }));
        }

        public async Task<Response<CategoryResultDto>> CreateAsync(CategoryCreationDto dto)
        {
            var newCategory = mapper.Map<WordCategory>(dto);
            unitOfWork.WordCategoryRepository.Add(newCategory);
            unitOfWork.WordCategoryRepository.SaveChanges();

            var resultDto = mapper.Map<CategoryResultDto>(newCategory);

            return new Response<CategoryResultDto>
            {
                StatusCode = 201,
                Message = "Word category created",
                Data = resultDto
            };
        }

        public async Task<Response<CategoryResultDto>> UpdateAsync(CategoryUpdateDto dto)
        {
            var existingCategory = unitOfWork.WordCategoryRepository.Select(dto.Id);
            if (existingCategory is null)
            {
                return new Response<CategoryResultDto>
                {
                    StatusCode = 404,
                    Message = "Word category not found",
                    Data = null
                };
            }

            existingCategory = mapper.Map<WordCategory>(dto);
            unitOfWork.WordCategoryRepository.Update(existingCategory);
            unitOfWork.WordCategoryRepository.SaveChanges();

            var resultDto = mapper.Map<CategoryResultDto>(existingCategory);

            return new Response<CategoryResultDto>
            {
                StatusCode = 200,
                Message = "Word category updated",
                Data = resultDto
            };
        }

        public async Task<Response<bool>> DeleteAsync(long id)
        {
            var existingCategory = unitOfWork.WordCategoryRepository.Select(id);
            if (existingCategory is null)
            {
                return new Response<bool>
                {
                    StatusCode = 404,
                    Message = "Word category not found",
                    Data = false
                };
            }

            unitOfWork.WordCategoryRepository.Delete(existingCategory);
            unitOfWork.WordCategoryRepository.SaveChanges();

            return new Response<bool>
            {
                StatusCode = 200,
                Message = "Word category deleted",
                Data = true
            };
        }

        public async Task<Response<WordCategory>> GetByIdAsync(long id)
        {
            var category = unitOfWork.WordCategoryRepository.Select(id);

            if (category is null)
            {
                return new Response<WordCategory>
                {
                    StatusCode = 404,
                    Message = "Word category not found",
                    Data = null
                };
            }

            return new Response<WordCategory>
            {
                StatusCode = 200,
                Message = "Word category returned",
                Data = category
            };
        }

        public async Task<Response<IEnumerable<WordCategory>>> GetAllAsync()
        {
            var allCategories = unitOfWork.WordCategoryRepository.SelectAll();

            if (allCategories is null)
            {
                return new Response<IEnumerable<WordCategory>>
                {
                    StatusCode = 404,
                    Message = "No word categories found",
                    Data = null
                };
            }

            return new Response<IEnumerable<WordCategory>>
            {
                StatusCode = 200,
                Message = "All word categories returned",
                Data = allCategories
            };
        }
    }
}
