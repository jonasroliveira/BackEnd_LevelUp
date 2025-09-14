using System.Collections.Generic;
using Newtonsoft.Json;
using BackEnd_LevelUp.Models;
using Xunit;

namespace BackEnd_LevelUp.Tests.Models;

public class FreeToGameModelTest
{
    [Fact]
    public void CanSetAndGetProperties()
    {
        var model = new FreeToGameModel
        {
            Id = 1,
            Title = "Test Game",
            Genre = "Action",
            Platform = "PC",
            GameUrl = "https://example.com/game"
        };

        Assert.Equal(1, model.Id);
        Assert.Equal("Test Game", model.Title);
        Assert.Equal("Action", model.Genre);
        Assert.Equal("PC", model.Platform);
        Assert.Equal("https://example.com/game", model.GameUrl);
    }

    [Fact]
    public void CanDeserializeFromJson()
    {
        var json = @"{
            ""id"": 2,
            ""title"": ""Another Game"",
            ""genre"": ""RPG"",
            ""platform"": ""Web"",
            ""game_url"": ""https://example.com/another""
        }";

        var model = JsonConvert.DeserializeObject<FreeToGameModel>(json);

        Assert.NotNull(model);
        Assert.Equal(2, model!.Id);
        Assert.Equal("Another Game", model.Title);
        Assert.Equal("RPG", model.Genre);
        Assert.Equal("Web", model.Platform);
        Assert.Equal("https://example.com/another", model.GameUrl);
    }
}

public class ExternalGameDetailTest
{
    [Fact]
    public void CanSetAndGetMinimumSystemRequirements()
    {
        var detail = new ExternalGameDetail
        {
            Id = 3,
            Title = "Detail Game",
            Genre = "Strategy",
            Platform = "PC",
            GameUrl = "https://example.com/detail",
            MinimumSystemRequirements = new MinimumSystemRequirements
            {
                Os = "Windows 10",
                Processor = "Intel i5",
                Memory = "8 GB",
                Graphics = "GTX 1050",
                Storage = "20 GB"
            }
        };

        Assert.Equal("Windows 10", detail.MinimumSystemRequirements!.Os);
        Assert.Equal("Intel i5", detail.MinimumSystemRequirements.Processor);
        Assert.Equal("8 GB", detail.MinimumSystemRequirements.Memory);
        Assert.Equal("GTX 1050", detail.MinimumSystemRequirements.Graphics);
        Assert.Equal("20 GB", detail.MinimumSystemRequirements.Storage);
    }

    [Fact]
    public void CanDeserializeMinimumSystemRequirementsFromJson()
    {
        var json = @"{
            ""id"": 4,
            ""title"": ""SysReq Game"",
            ""genre"": ""Adventure"",
            ""platform"": ""PC"",
            ""game_url"": ""https://example.com/sysreq"",
            ""minimum_system_requirements"": {
                ""os"": ""Linux"",
                ""processor"": ""AMD Ryzen"",
                ""memory"": ""16 GB"",
                ""graphics"": ""RX 580"",
                ""storage"": ""50 GB""
            }
        }";

        var detail = JsonConvert.DeserializeObject<ExternalGameDetail>(json);

        Assert.NotNull(detail);
        Assert.Equal(4, detail!.Id);
        Assert.Equal("SysReq Game", detail.Title);
        Assert.NotNull(detail.MinimumSystemRequirements);
        Assert.Equal("Linux", detail.MinimumSystemRequirements!.Os);
        Assert.Equal("AMD Ryzen", detail.MinimumSystemRequirements.Processor);
        Assert.Equal("16 GB", detail.MinimumSystemRequirements.Memory);
        Assert.Equal("RX 580", detail.MinimumSystemRequirements.Graphics);
        Assert.Equal("50 GB", detail.MinimumSystemRequirements.Storage);
    }
}