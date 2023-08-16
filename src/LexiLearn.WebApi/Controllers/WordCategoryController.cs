using LexiLearn.Service.DTOs.Categories;
using Microsoft.AspNetCore.Mvc;

namespace LexiLearn.Service.Interfaces;

[ApiController]
[Route("api/[controller]")]

public class WordCategoryController : ControllerBase
{
    private readonly IWordCategoryService _wordCategoryService;

    public WordCategoryController(IWordCategoryService wordCategoryService)
    {
        _wordCategoryService = wordCategoryService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreationDto dto)
    {
        var response = await _wordCategoryService.CreateAsync(dto);
        return Ok(response.Data);
    }

    [HttpPut]
    public async Task<IActionResult> Update(CategoryUpdateDto dto)
    {
        var response = await _wordCategoryService.UpdateAsync(dto);

        if (response.Data is null)
            return NotFound(response.Data);
        return Ok(response.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var response = await _wordCategoryService.DeleteAsync(id);
        return Ok(response.Data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await _wordCategoryService.GetByIdAsync(id);

        if (response.Data is null)
            return NotFound(response.Data);
        return Ok(response.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _wordCategoryService.GetAllAsync();
        return Ok(response.Data);
    }
}