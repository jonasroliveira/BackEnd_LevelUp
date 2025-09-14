using BackEnd_LevelUp.Models;

namespace BackEnd_LevelUp.Interfaces;

public interface IGameRecommendationRepository
{
    Task AddAsync(GameRecommendationModel rec);
    Task<List<GameRecommendationModel>> GetAllAsync();
}