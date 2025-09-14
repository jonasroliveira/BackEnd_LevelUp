using Microsoft.AspNetCore.Mvc;
using BackEnd_LevelUp.DTOs;
using BackEnd_LevelUp.Interfaces;
using BackEnd_LevelUp.Models;
using FluentValidation;
using BackEnd_LevelUp.DtOs;

namespace BackEnd_LevelUp.Controllers;

[ApiController]
[Route("/api")]

public class RecommendController : ControllerBase
{

    private readonly IFreeToGameClient _freeToGame;
    private readonly IGameRecommendationRepository _repo;
    private readonly IValidator<RecommendationRequestDto> _validator;

    public RecommendController(
        IFreeToGameClient freeToGame,
        IGameRecommendationRepository repo,
        IValidator<RecommendationRequestDto> validator)
    {
        _freeToGame = freeToGame;
        _repo = repo;
        _validator = validator;
    }

    [HttpPost("recommend")]
    [ProducesResponseType(typeof(List<RecommendationResponseDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Recommend([FromBody] RecommendationRequestDto req)
    {
        var vr = await _validator.ValidateAsync(req);
        if (!vr.IsValid)
            return BadRequest(vr.Errors.Select(e => e.ErrorMessage));

        var tag = string.Join('.', req.Genres.Select(g => g.Trim().ToLowerInvariant()));
        string? platformParam = null;
        if (!string.IsNullOrWhiteSpace(req.Platform))
        {
            var p = req.Platform.Trim().ToLowerInvariant();
            if (p == "pc" || p == "browser") platformParam = p;
        }

        var candidateSummaries = await _freeToGame.FilterGamesAsync(tag, platformParam);
        if (candidateSummaries == null || candidateSummaries.Count == 0)
            return NotFound(new { message = "No games found with the filters provided." });

        var validGames = candidateSummaries;

        foreach (var g in validGames)
        {
            var rec = new GameRecommendationModel
            {
                GameTitle = g.Title,
                Category = g.Genre ?? (req.Genres.FirstOrDefault() ?? "Unknown"),
                RecommendedAt = DateTime.UtcNow
            };
            await _repo.AddAsync(rec);
        }

        var response = validGames.Select(g => new RecommendationResponseDto
        {
            Title = g.Title,
            Url = g.GameUrl
        }).ToList();

        return Ok(response);
    }
}