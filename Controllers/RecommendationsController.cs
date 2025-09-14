using Microsoft.AspNetCore.Mvc;
using BackEnd_LevelUp.DTOs;
using BackEnd_LevelUp.Interfaces;
using BackEnd_LevelUp.Models;

namespace BackEnd_LevelUp.Controllers;

[ApiController]
[Route("/api")]
public class RecommendationsController : ControllerBase
{    
    private readonly IGameRecommendationRepository _repo;
    

    public RecommendationsController( 
        IGameRecommendationRepository repo)
    {       
        _repo = repo;
    }

    [HttpGet("recommendations")]
    [ProducesResponseType(typeof(List<GameRecommendationModel>), 200)]
    public async Task<IActionResult> GetRecommendations()
    {
        var all = await _repo.GetAllAsync();
        return Ok(all);
    }
}