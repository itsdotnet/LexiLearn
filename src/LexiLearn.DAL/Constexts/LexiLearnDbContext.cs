using LexiLearn.DAL.Constans;
using LexiLearn.Domain.Entities.Categories;
using LexiLearn.Domain.Entities.Questions;
using LexiLearn.Domain.Entities.Quizzes;
using LexiLearn.Domain.Entities.Words;
using LexiLearn.Domain.Entity.User;
using Microsoft.EntityFrameworkCore;

namespace LexiLearn.DAL.Constexts;

public class LexiLearnDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Quiz> Quizzes { get; set; }

    public DbSet<Word> Words { get; set; }

    public DbSet<Question> Questions { get; set; }

    public DbSet<QuizCategory> QuizCategories { get; set; }

    public DbSet<WordCategory> WordCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(DbConstans.CONNECTION_STRING);
    }
}
