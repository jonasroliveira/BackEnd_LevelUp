using BackEnd_LevelUp.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_LevelUp.Data;

public class AppDbContext : DbContext
{
    public DbSet<GameRecommendationModel> GameRecommendations { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameRecommendationModel>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.GameTitle).IsRequired();
            b.Property(x => x.Category).IsRequired();
        });
    }
}