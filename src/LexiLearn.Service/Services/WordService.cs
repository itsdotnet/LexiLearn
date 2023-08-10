using AutoMapper;
using LexiLearn.DAL.IRepositories;
using LexiLearn.DAL.Repository;
using LexiLearn.Domain.Entities.Words;
using LexiLearn.Service.DTOs.Quizzes;
using LexiLearn.Service.DTOs.Words;
using LexiLearn.Service.Helpers;
using LexiLearn.Service.Interfaces;
using LexiLearn.Service.Mappers;

namespace LexiLearn.Service.Services;

public class WordService : IWordService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public WordService()
    {
        unitOfWork = new UnitOfWork();
        mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        }));
    }

    public async Task<Response<WordResultDto>> CreateAsync(WordCreationDto dto)
    {
        if ((await IsValidCategoryId(dto.CategoryId)).Data)
            return new Response<WordResultDto>()
            {
                StatusCode = 400,
                Message = "Category ID invalid",
                Data = null
            };

        var newWord = mapper.Map<Word>(dto);
        unitOfWork.WordRepository.Add(newWord);

        unitOfWork.WordRepository.SaveChanges();


        var resultDto = mapper.Map<WordResultDto>(newWord);

        return new Response<WordResultDto>
        {
            StatusCode = 201,
            Message = "Word created",
            Data = resultDto
        };
    }

    public async Task<Response<WordResultDto>> UpdateAsync(WordUpdateDto dto)
    {
        if ((await IsValidCategoryId(dto.CategoryId)).Data)
            return new Response<WordResultDto>()
            {
                StatusCode = 400,
                Message = "Category ID invalid",
                Data = null
            };

        var existingWord = unitOfWork.WordRepository.Select(dto.Id);

        if (existingWord is null)
        {
            return new Response<WordResultDto>
            {
                StatusCode = 404,
                Message = "Word not found",
                Data = null
            };
        }


        mapper.Map(dto, existingWord);
        unitOfWork.WordRepository.Update(existingWord);
        unitOfWork.WordRepository.SaveChanges();

        var resultDto = mapper.Map<WordResultDto>(existingWord);

        return new Response<WordResultDto>
        {
            StatusCode = 200,
            Message = "Word updated",
            Data = resultDto
        };
    }

    public async Task<Response<bool>> DeleteAsync(long id)
    {
        var existingWord = unitOfWork.WordRepository.Select(id);

        if (existingWord is null)
        {
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "Word not found",
                Data = false
            };
        }

        unitOfWork.WordRepository.Delete(existingWord);
        unitOfWork.WordRepository.SaveChanges();

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Word deleted",
            Data = true
        };
    }

    public async Task<Response<Word>> GetByIdAsync(long id)
    {
        var word = unitOfWork.WordRepository.Select(id);

        if (word is null)
        {
            return new Response<Word>
            {
                StatusCode = 404,
                Message = "Word not found",
                Data = null
            };
        }

        return new Response<Word>
        {
            StatusCode = 200,
            Message = "Word returned",
            Data = word
        };
    }

    public async Task<Response<IEnumerable<Word>>> GetAllAsync()
    {
        var allWords = unitOfWork.WordRepository.SelectAll();

        if (allWords is null)
        {
            return new Response<IEnumerable<Word>>
            {
                StatusCode = 404,
                Message = "No words found",
                Data = null
            };
        }

        return new Response<IEnumerable<Word>>
        {
            StatusCode = 200,
            Message = "All words returned",
            Data = allWords
        };
    }

    public async Task<Response<IEnumerable<Word>>> GetWordsByCategoryAsync(long categoryId)
    {
        var wordsInCategory = unitOfWork.WordRepository.SelectAll()
            .Where(w => w.CategoryId == categoryId);

        if (wordsInCategory == null || !wordsInCategory.Any())
        {
            return new Response<IEnumerable<Word>>
            {
                StatusCode = 404,
                Message = "No words found in the specified category",
                Data = null
            };
        }

        return new Response<IEnumerable<Word>>
        {
            StatusCode = 200,
            Message = "Words in the specified category returned",
            Data = wordsInCategory
        };
    }

    public async Task<Response<IEnumerable<Word>>> SearchWordsAsync(string searchTerm)
    {
        var searchedWords = unitOfWork.WordRepository.SelectAll().Where(
            w => w.Text.StartsWith(searchTerm.ToLower().Trim())
                ||
                    w.Translation.StartsWith(searchTerm.ToLower().Trim()));

        if (searchedWords is null)
        {
            return new Response<IEnumerable<Word>>
            {
                StatusCode = 404,
                Message = "No words found for the specified search term",
                Data = null
            };
        }

        return new Response<IEnumerable<Word>>
        {
            StatusCode = 200,
            Message = "Words matching the search term returned",
            Data = searchedWords
        };
    }

    public async Task<Response<bool>> IsValidCategoryId(long categoryId)
    {
        var category = unitOfWork.WordCategoryRepository.Select(categoryId);

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