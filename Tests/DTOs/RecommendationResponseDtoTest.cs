using BackEnd_LevelUp.DtOs;
using Xunit;

namespace BackEnd_LevelUp.Tests.DtOs;

public class RecommendationResponseDtoTest
{
    [Fact]
    public void CanSetAndGetProperties()
    {
        var dto = new RecommendationResponseDto
        {
            Title = "Game Title",
            Url = "https://example.com/game"
        };

        Assert.Equal("Game Title", dto.Title);
        Assert.Equal("https://example.com/game", dto.Url);
    }
}