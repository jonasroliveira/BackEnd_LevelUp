using BackEnd_LevelUp.Data;
using BackEnd_LevelUp.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BackEnd_LevelUp.Tests.Data;

public class AppDbContextTest
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task CanAddAndRetrieveGameRecommendation()
    {
        using var context = GetInMemoryDbContext();
        var rec = new GameRecommendationModel
        {
            GameTitle = "Test Game",
            Category = "Action",
            RecommendedAt = DateTime.UtcNow
        };
        context.GameRecommendations.Add(rec);
        await context.SaveChangesAsync();

        var saved = await context.GameRecommendations.FirstOrDefaultAsync();
        Assert.NotNull(saved);
        Assert.Equal("Test Game", saved!.GameTitle);
        Assert.Equal("Action", saved.Category);
    }

    [Fact]
    public async Task GameTitle_IsRequired()
    {
        using var context = GetInMemoryDbContext();
        var rec = new GameRecommendationModel
        {
            Category = "Action",
            RecommendedAt = DateTime.UtcNow
        };
        context.GameRecommendations.Add(rec);
        await Assert.ThrowsAsync<DbUpdateException>(() => context.SaveChangesAsync());
    }

    [Fact]
    public async Task Category_IsRequired()
    {
        using var context = GetInMemoryDbContext();
        var rec = new GameRecommendationModel
        {
            GameTitle = "Test Game",
            RecommendedAt = DateTime.UtcNow
        };
        context.GameRecommendations.Add(rec);
        await Assert.ThrowsAsync<DbUpdateException>(() => context.SaveChangesAsync());
    }

    [Fact]
    public void Id_IsPrimaryKey()
    {
        using var context = GetInMemoryDbContext();
        var entityType = context.Model.FindEntityType(typeof(GameRecommendationModel));
        var pk = entityType.FindPrimaryKey();
        Assert.Single(pk.Properties);
        Assert.Equal("Id", pk.Properties[0].Name);
    }
}