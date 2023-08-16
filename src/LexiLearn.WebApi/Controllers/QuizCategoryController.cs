using LexiLearn.Service.DTOs.Categories;
using LexiLearn.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LexiLearn.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class QuizCategoryController : ControllerBase
{
    private readonly IQuizCategoryService _quizCategoryService;

    public QuizCategoryController(IQuizCategoryService quizCategoryService)
    {
        _quizCategoryService = quizCategoryService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreationDto dto)
    {
        var response = await _quizCategoryService.CreateAsync(dto);
        return Ok(response.Data);
    }

    [HttpPut]
    public async Task<IActionResult> Update(CategoryUpdateDto dto)
    {
        var response = await _quizCategoryService.UpdateAsync(dto);
        
        if(response.Data is null)
            return NotFound(response.Data);
        return Ok(response.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var response = await _quizCategoryService.DeleteAsync(id);
        return Ok(response.Data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await _quizCategoryService.GetByIdAsync(id);

        if (response.Data is null)
            return NotFound(response.Data);
        return Ok(response.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _quizCategoryService.GetAllAsync();
        return Ok(response.Data);
    }
}