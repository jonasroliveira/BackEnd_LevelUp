using BackEnd_LevelUp.DTOs;
using Xunit;

namespace BackEnd_LevelUp.Tests.DTOs;

public class RecommendationRequestDtoTest
{
    [Fact]
    public void CanInstantiateWithDefaults()
    {
        var dto = new RecommendationRequestDto();
        Assert.NotNull(dto.Genres);
        Assert.Empty(dto.Genres);
        Assert.Null(dto.Platform);
        Assert.Null(dto.MinRamGb);
    }

    [Fact]
    public void CanSetProperties()
    {
        var dto = new RecommendationRequestDto
        {
            Genres = new List<string> { "Action", "RPG" },
            Platform = "PC",
            MinRamGb = 8
        };

        Assert.Equal(new List<string> { "Action", "RPG" }, dto.Genres);
        Assert.Equal("PC", dto.Platform);
        Assert.Equal(8, dto.MinRamGb);
    }
}