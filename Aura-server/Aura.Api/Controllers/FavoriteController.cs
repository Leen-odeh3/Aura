using Aura.Application.Abstracts;
using Aura.Application.Abstracts.UserServices;
using Aura.Domain.DTOs.Favorite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aura.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FavoriteController : ControllerBase
{
    private readonly IFavoriteService _favoriteService;
    private readonly IAuthenticatedUserService _authenticatedUserService;

    public FavoriteController(IFavoriteService favoriteService, IAuthenticatedUserService authenticatedUserService)
    {
        _favoriteService = favoriteService;
        _authenticatedUserService = authenticatedUserService;
    }

    [HttpGet("userFavorites")]
    public async Task<IActionResult> GetFavoritesByAuthenticatedUser()
    {
        var userId = _authenticatedUserService.GetAuthenticatedUserId();

        if (userId <= 0)
        {
            return Unauthorized("User is not authenticated.");
        }

        var favorites = await _favoriteService.GetFavoritesByUserIdAsync(userId);

        if (favorites == null || !favorites.Any())
        {
            return NotFound("No favorites found for this user.");
        }

        return Ok(favorites);
    }

    // POST: api/Favorite
    [HttpPost]
    public async Task<IActionResult> AddFavorite([FromBody] FavoriteRequestDto request)
    {
        var userId = _authenticatedUserService.GetAuthenticatedUserId();

        if (userId <= 0)
        {
            return Unauthorized("User is not authenticated.");
        }

        var favorite = await _favoriteService.AddFavoriteAsync(request.PostId, userId);

        return CreatedAtAction(nameof(GetFavoritesByAuthenticatedUser), new { postId = request.PostId }, favorite);
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
