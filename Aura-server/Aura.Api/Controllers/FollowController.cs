using Aura.Application.Abstracts;
using Aura.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aura.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FollowController : ControllerBase
{
    private readonly IFollowService _followService;

    public FollowController(IFollowService followService)
    {
        _followService = followService;
    }

    [HttpPost("follow")]
    public async Task<IActionResult> FollowUser(int followerId, int followingId)
    {
        await _followService.FollowUserAsync(followerId, followingId);
        return Ok(new { Message = "Successfully followed the user." });
    }

    [HttpPost("unfollow")]
    public async Task<IActionResult> UnfollowUser(int followerId, int followingId)
    {
        await _followService.UnfollowUserAsync(followerId, followingId);
        return Ok(new { Message = "Successfully unfollowed the user." });
    }

    [HttpGet("followers/{userId}")]
    public async Task<IActionResult> GetFollowers(int userId)
    {
        var followers = await _followService.GetFollowersAsync(userId);
        var result = followers.Select(f => new
        {
            f.Id,
            f.Username,
            f.Image
        });

        return Ok(result);
    }

    [HttpGet("following/{userId}")]
    public async Task<IActionResult> GetFollowing(int userId)
    {
        var following = await _followService.GetFollowingAsync(userId);
        var result = following.Select(f => new
        {
            f.Id,
            f.Username,
            f.Image
        });

        return Ok(result);
    }
}