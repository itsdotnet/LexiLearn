using LexiLearn.DAL.Constans;
using Microsoft.EntityFrameworkCore;

namespace LexiLearn.DAL.Constexts;

public class LexiLearnDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(DbConstans.CONNECTION_STRING);
    }
}
