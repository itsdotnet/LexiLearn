using LexiLearn.DAL.Constexts;
using LexiLearn.Domain.Services;
using LexiLearn.Service.Interfaces;
using LexiLearn.Service.Mappers;
using LexiLearn.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace LexiLearn.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<LexiLearnDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IQuizService, QuizService>();
        builder.Services.AddScoped<IWordService, WordService>();
        builder.Services.AddScoped<IQuestionService, QuestionService>();
        builder.Services.AddScoped<IQuizHistoryService, QuizHistoryService>();
        builder.Services.AddScoped<IQuizCategoryService, QuizCategoryService>();
        builder.Services.AddScoped<IWordCategoryService, WordCategoryService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}