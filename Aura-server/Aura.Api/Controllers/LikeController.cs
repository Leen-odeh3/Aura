using Aura.Application.Abstracts;
using Aura.Domain.DTOs.Like;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aura.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LikeController : ControllerBase
{
    private readonly ILikeService _likeService;

    public LikeController(ILikeService likeService)
    {
        _likeService = likeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<LikeResponseDto>>> GetLikesByPostId(int postId)
    {
        var likes = await _likeService.GetLikesByPostIdAsync(postId);
        return Ok(likes);
    }

    [HttpPost]
    public async Task<IActionResult> AddLike(int postId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized("User is not authenticated.");
        }

        var like = await _likeService.AddLikeAsync(postId, Convert.ToInt32(userId));
        return Ok(like);
    }

    [HttpDelete("{likeId}")]
    public async Task<IActionResult> RemoveLike(int likeId)
    {
        var like = await _likeService.RemoveLikeAsync(likeId);
        if (like == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}