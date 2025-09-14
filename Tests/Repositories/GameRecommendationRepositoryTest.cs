using BackEnd_LevelUp.Data;
using BackEnd_LevelUp.Models;
using BackEnd_LevelUp.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BackEnd_LevelUp.Tests.Repositories;

public class GameRecommendationRepositoryTest
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task AddAsync_AddsNewRecommendation_WhenNotExists()
    {
        using var context = GetInMemoryDbContext();
        var repo = new GameRecommendationRepository(context);

        var rec = new GameRecommendationModel
        {
            GameTitle = "Test Game",
            Category = "Action",
            RecommendedAt = DateTime.UtcNow
        };

        await repo.AddAsync(rec);

        var saved = await context.GameRecommendations.FirstOrDefaultAsync();
        Assert.NotNull(saved);
        Assert.Equal("Test Game", saved!.GameTitle);
        Assert.Equal("Action", saved.Category);
    }

    [Fact]
    public async Task AddAsync_DoesNotAddDuplicateRecommendation()
    {
        using var context = GetInMemoryDbContext();
        var repo = new GameRecommendationRepository(context);

        var rec = new GameRecommendationModel
        {
            GameTitle = "Test Game",
            Category = "Action",
            RecommendedAt = DateTime.UtcNow
        };

        await repo.AddAsync(rec);
        await repo.AddAsync(rec);

        var count = await context.GameRecommendations.CountAsync();
        Assert.Equal(1, count);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsRecommendationsOrderedByDateDesc()
    {
        using var context = GetInMemoryDbContext();
        var repo = new GameRecommendationRepository(context);

        var rec1 = new GameRecommendationModel
        {
            GameTitle = "Game 1",
            Category = "Puzzle",
            RecommendedAt = DateTime.UtcNow.AddDays(-1)
        };
        var rec2 = new GameRecommendationModel
        {
            GameTitle = "Game 2",
            Category = "Action",
            RecommendedAt = DateTime.UtcNow
        };

        await repo.AddAsync(rec1);
        await repo.AddAsync(rec2);

        var all = await repo.GetAllAsync();
        Assert.Equal(2, all.Count);
        Assert.Equal("Game 2", all[0].GameTitle);
        Assert.Equal("Game 1", all[1].GameTitle);
    }
}