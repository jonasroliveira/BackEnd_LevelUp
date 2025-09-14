using Newtonsoft.Json;

namespace BackEnd_LevelUp.Models;

public class FreeToGameModel
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("title")]
    public string Title { get; set; } = null!;
    [JsonProperty("genre")]
    public string Genre { get; set; } = null!;
    [JsonProperty("platform")]
    public string Platform { get; set; } = null!;
    [JsonProperty("game_url")]
    public string GameUrl { get; set; } = null!;
}

public class ExternalGameDetail : FreeToGameModel
{
    [JsonProperty("minimum_system_requirements")]
    public MinimumSystemRequirements? MinimumSystemRequirements { get; set; }
}

public class MinimumSystemRequirements
{
    [JsonProperty("os")]
    public string? Os { get; set; }
    [JsonProperty("processor")]
    public string? Processor { get; set; }
    [JsonProperty("memory")]
    public string? Memory { get; set; }
    [JsonProperty("graphics")]
    public string? Graphics { get; set; }
    [JsonProperty("storage")]
    public string? Storage { get; set; }
}