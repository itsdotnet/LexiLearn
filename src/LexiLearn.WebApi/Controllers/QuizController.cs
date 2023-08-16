using LexiLearn.Domain.Enums;
using LexiLearn.Service.DTOs.Quizzes;
using LexiLearn.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LexiLearn.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(QuizCreationDto dto)
    {
        var response = await _quizService.CreateAsync(dto);
        return Ok(response.Data);
    }

    [HttpPut]
    public async Task<IActionResult> Update(QuizUpdateDto dto)
    {
        var response = await _quizService.UpdateAsync(dto);
        
        if (response.Data is null)
            return NotFound(response.Data);
        return Ok(response.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var response = await _quizService.DeleteAsync(id);
        return Ok(response.Data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await _quizService.GetByIdAsync(id);

        if (response.Data is null)
            return NotFound(response.Data);
        return Ok(response.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _quizService.GetAllAsync();
        return Ok(response.Data);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetQuizzesByCategory(long categoryId)
    {
        var response = await _quizService.GetQuizzesByCategoryAsync(categoryId);
        return Ok(response.Data);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchQuizzes(string searchTerm)
    {
        var response = await _quizService.SearchQuizzesAsync(searchTerm);
        return Ok(response.Data);
    }

    [HttpGet("level/{level}")]
    public async Task<IActionResult> GetQuizzesByLevel(QuizLevel level)
    {
        var response = await _quizService.GetQuizzesByLevelAsync(level);
        return Ok(response.Data);
    }

    [HttpGet("category/valid/{categoryId}")]
    public async Task<IActionResult> IsValidCategoryId(long categoryId)
    {
        var response = await _quizService.IsValidCategoryId(categoryId);
        return Ok(response.Data);
    }
}