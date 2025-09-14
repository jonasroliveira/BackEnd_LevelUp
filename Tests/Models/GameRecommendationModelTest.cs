using System;
using BackEnd_LevelUp.Models;
using Xunit;

namespace BackEnd_LevelUp.Tests.Models;

public class GameRecommendationModelTest
{
    [Fact]
    public void CanSetAndGetProperties()
    {
        var now = DateTime.UtcNow;
        var model = new GameRecommendationModel
        {
            Id = 1,
            GameTitle = "Test Game",
            Category = "Puzzle",
            RecommendedAt = now
        };

        Assert.Equal(1, model.Id);
        Assert.Equal("Test Game", model.GameTitle);
        Assert.Equal("Puzzle", model.Category);
        Assert.Equal(now, model.RecommendedAt);
    }

    [Fact]
    public void DefaultRecommendedAtIsUtcNow()
    {
        var before = DateTime.UtcNow.AddSeconds(-1);
        var model = new GameRecommendationModel();
        var after = DateTime.UtcNow.AddSeconds(1);

        Assert.True(model.RecommendedAt >= before && model.RecommendedAt <= after);
    }
}