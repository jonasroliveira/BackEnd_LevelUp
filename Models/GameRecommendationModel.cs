namespace BackEnd_LevelUp.Models;

public class GameRecommendationModel
{
    public int Id { get; set; }
    public string GameTitle { get; set; } = null!;
    public string Category { get; set; } = null!;
    public DateTime RecommendedAt { get; set; } = DateTime.UtcNow;
}