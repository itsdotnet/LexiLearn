using LexiLearn.Service.DTOs.Words;
using LexiLearn.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LexiLearn.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class WordController : ControllerBase
{
    private readonly IWordService _wordService;

    public WordController(IWordService wordService)
    {
        _wordService = wordService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(WordCreationDto dto)
    {
        var response = await _wordService.CreateAsync(dto);
        return Ok(response.Data);
    }

    [HttpPut]
    public async Task<IActionResult> Update(WordUpdateDto dto)
    {
        var response = await _wordService.UpdateAsync(dto);

        if (response.Data is null)
            return NotFound(response.Data);
        return Ok(response.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var response = await _wordService.DeleteAsync(id);
        return Ok(response.Data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await _wordService.GetByIdAsync(id);

        if (response.Data is null)
            return NotFound(response.Data);
        return Ok(response.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _wordService.GetAllAsync();
        return Ok(response.Data);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetWordsByCategory(long categoryId)
    {
        var response = await _wordService.GetWordsByCategoryAsync(categoryId);
        return Ok(response.Data);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchWords(string searchTerm)
    {
        var response = await _wordService.SearchWordsAsync(searchTerm);
        return Ok(response.Data);
    }

    [HttpGet("category/valid/{categoryId}")]
    public async Task<IActionResult> IsValidCategoryId(long categoryId)
    {
        var response = await _wordService.IsValidCategoryId(categoryId);
        return Ok(response.Data);
    }
}