using Aura.Application.Abstracts;
using Aura.Domain.DTOs.Favorite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aura.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FavoriteController : ControllerBase
{
    private readonly IFavoriteService _favoriteService;

    public FavoriteController(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }

    [HttpGet("{postId}")]
    public async Task<IActionResult> GetFavoritesByPostId(int postId)
    {
        var favorites = await _favoriteService.GetFavoritesByPostIdAsync(postId);
        return Ok(favorites);
    }

    [HttpPost]
    public async Task<IActionResult> AddFavorite([FromBody] FavoriteRequestDto request)
    {
        var favorite = await _favoriteService.AddFavoriteAsync(request.PostId, request.UserId);
        return CreatedAtAction(nameof(GetFavoritesByPostId), new { postId = request.PostId }, favorite);
    }

    [HttpDelete("{favoriteId}")]
    public async Task<IActionResult> RemoveFavorite(int favoriteId)
    {
        var removedFavorite = await _favoriteService.RemoveFavoriteAsync(favoriteId);
        if (removedFavorite == null)
        {
            return NotFound("Favorite not found.");
        }
        return Ok(removedFavorite);
    }
}