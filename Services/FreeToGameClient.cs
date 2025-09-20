using BackEnd_LevelUp.Models;
using BackEnd_LevelUp.Interfaces;
using Newtonsoft.Json;

namespace BackEnd_LevelUp.Services;

public class FreeToGameClient : IFreeToGameClient
{
    private readonly HttpClient _http;

    public FreeToGameClient(HttpClient http)
    {
        _http = http;
        _http.BaseAddress ??= new Uri("https://www.freetogame.com/api/");
    }

    public async Task<IEnumerable<FreeToGameModel>> FilterGamesAsync(string? tag, string? platform)
    {        
        var query = new List<string>();
        if (!string.IsNullOrWhiteSpace(tag))
            query.Add($"tag={Uri.EscapeDataString(tag)}");
        if (!string.IsNullOrWhiteSpace(platform) && platform != "both")
            query.Add($"platform={Uri.EscapeDataString(platform)}");

        var q = query.Count > 0 ? "filter?" + string.Join("&", query) : "games";
        var url = q.StartsWith("filter") ? $"filter?{string.Join("&", query)}" : "games";

        var resp = await _http.GetAsync(url);
        if (!resp.IsSuccessStatusCode)
        {
            return new List<FreeToGameModel>();
        }

        var content = await resp.Content.ReadAsStringAsync();
        var list = JsonConvert.DeserializeObject<List<FreeToGameModel>>(content) ?? new List<FreeToGameModel>();
        return list;
    }

    public async Task<ExternalGameDetail?> GetGameDetailsAsync(int id)
    {
        var resp = await _http.GetAsync($"game?id={id}");
        if (!resp.IsSuccessStatusCode) return null;
        var content = await resp.Content.ReadAsStringAsync();
        var detail = JsonConvert.DeserializeObject<ExternalGameDetail>(content);
        return detail;
    }
}