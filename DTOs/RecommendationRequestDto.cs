namespace BackEnd_LevelUp.DTOs;

public class RecommendationRequestDto
{
    public List<string> Genres { get; set; } = new List<string>();
    public string? Platform { get; set; }
    public int? MinRamGb { get; set; }
}