using AutoMapper;
using LexiLearn.DAL.IRepositories;
using LexiLearn.DAL.Repository;
using LexiLearn.Domain.Entities.Words;
using LexiLearn.Domain.Enums;
using LexiLearn.Service.DTOs.Words;
using LexiLearn.Service.Helpers;
using LexiLearn.Service.Interfaces;
using LexiLearn.Service.Mappers;

namespace LexiLearn.Service.Services
{
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
            var newWord = mapper.Map<Word>(dto);
            unitOfWork.WordRepository.Add(newWord);

            await unitOfWork.SaveAsync();
            

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
            var existingWord = await unitOfWork.WordRepository.GetByIdAsync(dto.Id);
            if (existingWord == null)
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
            await unitOfWork.SaveAsync();

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
            var existingWord = await unitOfWork.WordRepository.GetByIdAsync(id);
            if (existingWord == null)
            {
                return new Response<bool>
                {
                    StatusCode = 404,
                    Message = "Word not found",
                    Data = false
                };
            }

            unitOfWork.WordRepository.Delete(existingWord);
            await unitOfWork.SaveAsync();

            return new Response<bool>
            {
                StatusCode = 200,
                Message = "Word deleted",
                Data = true
            };
        }

        public async Task<Response<Word>> GetByIdAsync(long id)
        {
            var word = await unitOfWork.WordRepository.GetByIdAsync(id);

            if (word == null)
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
            var allWords = await unitOfWork.WordRepository.GetAllAsync();

            if (allWords == null || !allWords.Any())
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
            var wordsInCategory = await unitOfWork.WordRepository.GetWordsByCategoryAsync(categoryId);

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
            var searchedWords = await unitOfWork.WordRepository.SearchWordsAsync(searchTerm);

            if (searchedWords == null || !searchedWords.Any())
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

        public async Task<Response<IEnumerable<Word>>> GetWordsByLevelAsync(WordLevel level)
        {
            var wordsByLevel = await unitOfWork.WordRepository.GetWordsByLevelAsync(level);

            if (wordsByLevel == null || !wordsByLevel.Any())
            {
                return new Response<IEnumerable<Word>>
                {
                    StatusCode = 404,
                    Message = "No words found for the specified level",
                    Data = null
                };
            }

            return new Response<IEnumerable<Word>>
            {
                StatusCode = 200,
                Message = "Words for the specified level returned",
                Data = wordsByLevel
            };
        }
    }
}
