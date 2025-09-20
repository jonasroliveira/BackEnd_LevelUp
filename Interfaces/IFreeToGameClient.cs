using BackEnd_LevelUp.Models;

namespace BackEnd_LevelUp.Interfaces;

public interface IFreeToGameClient
{
    Task<IEnumerable<FreeToGameModel>> FilterGamesAsync(string? tag, string? platform);
    Task<ExternalGameDetail?> GetGameDetailsAsync(int id);
}
