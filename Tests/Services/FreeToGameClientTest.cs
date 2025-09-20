using System.Net;
using BackEnd_LevelUp.Models;
using BackEnd_LevelUp.Services;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Xunit;
using System.Linq;

namespace BackEnd_LevelUp.Tests.Services;

public class FreeToGameClientTest
{
    private HttpClient CreateMockHttpClient(HttpStatusCode statusCode, string responseContent, Action<HttpRequestMessage>? requestInspector = null)
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        handlerMock
           .Protected()
           .Setup<Task<HttpResponseMessage>>(
               "SendAsync",
               ItExpr.IsAny<HttpRequestMessage>(),
               ItExpr.IsAny<CancellationToken>()
           )
           .Callback<HttpRequestMessage, CancellationToken>((req, ct) => requestInspector?.Invoke(req))
           .ReturnsAsync(new HttpResponseMessage
           {
               StatusCode = statusCode,
               Content = new StringContent(responseContent),
           })
           .Verifiable();

        return new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://www.freetogame.com/api/")
        };
    }

    [Fact]
    public async Task FilterGamesAsync_ReturnsList_WhenSuccess()
    {
        var games = new List<FreeToGameModel>
        {
            new FreeToGameModel { Id = 1, Title = "Game1", Genre = "Action", Platform = "PC", GameUrl = "url1" },
            new FreeToGameModel { Id = 2, Title = "Game2", Genre = "RPG", Platform = "Web", GameUrl = "url2" }
        };
        var json = JsonConvert.SerializeObject(games);

        var httpClient = CreateMockHttpClient(HttpStatusCode.OK, json);
        var client = new FreeToGameClient(httpClient);

        var result = await client.FilterGamesAsync("Action", "PC");

        Assert.NotNull(result);
        var resultList = result.ToList();
        Assert.Equal(2, resultList.Count);
        Assert.Equal("Game1", resultList[0].Title);
        Assert.Equal("Game2", resultList[1].Title);
    }

    [Fact]
    public async Task FilterGamesAsync_ReturnsEmptyList_WhenNotSuccess()
    {
        var httpClient = CreateMockHttpClient(HttpStatusCode.BadRequest, "");
        var client = new FreeToGameClient(httpClient);

        var result = await client.FilterGamesAsync(null, null);

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetGameDetailsAsync_ReturnsDetail_WhenSuccess()
    {
        var detail = new ExternalGameDetail
        {
            Id = 10,
            Title = "Detail Game",
            Genre = "Adventure",
            Platform = "PC",
            GameUrl = "url10",
            MinimumSystemRequirements = new MinimumSystemRequirements
            {
                Os = "Windows",
                Processor = "i5",
                Memory = "8 GB",
                Graphics = "GTX 1050",
                Storage = "20 GB"
            }
        };
        var json = JsonConvert.SerializeObject(detail);

        var httpClient = CreateMockHttpClient(HttpStatusCode.OK, json);
        var client = new FreeToGameClient(httpClient);

        var result = await client.GetGameDetailsAsync(10);

        Assert.NotNull(result);
        Assert.Equal(10, result!.Id);
        Assert.Equal("Detail Game", result.Title);
        Assert.NotNull(result.MinimumSystemRequirements);
        Assert.Equal("Windows", result.MinimumSystemRequirements!.Os);
    }

    [Fact]
    public async Task GetGameDetailsAsync_ReturnsNull_WhenNotSuccess()
    {
        var httpClient = CreateMockHttpClient(HttpStatusCode.NotFound, "");
        var client = new FreeToGameClient(httpClient);

        var result = await client.GetGameDetailsAsync(999);

        Assert.Null(result);
    }
}