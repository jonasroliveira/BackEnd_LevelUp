using BackEnd_LevelUp.Data;
using BackEnd_LevelUp.Interfaces;
using BackEnd_LevelUp.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_LevelUp.Repositories;

public class GameRecommendationRepository : IGameRecommendationRepository
{
    private readonly AppDbContext _ctx;
    public GameRecommendationRepository(AppDbContext ctx) { _ctx = ctx; }

    public async Task AddAsync(GameRecommendationModel rec)
    {
        bool exists = await _ctx.GameRecommendations
        .AnyAsync(g => g.GameTitle == rec.GameTitle && g.Category == rec.Category);

        if (!exists)
        {
            _ctx.GameRecommendations.Add(rec);
            await _ctx.SaveChangesAsync();
        }
    }

    public async Task<List<GameRecommendationModel>> GetAllAsync()
    {
        return await _ctx.GameRecommendations
            .AsNoTracking()
            .OrderByDescending(x => x.RecommendedAt)
            .ToListAsync();
    }
}