using Aura.Application.Abstracts;
using Aura.Domain.DTOs.Comment;
using Aura.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aura.Api.Controllers;
[Route("api/posts/{postId}/comments")]
[ApiController]
[Authorize]

public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Comment>>> GetCommentsByPostId(int postId)
    {
        var comments = await _commentService.GetCommentsByPostIdAsync(postId);
        return Ok(comments);
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(int postId, [FromBody] PostComment commentDto)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdString is null)
        {
            return Unauthorized("User is not authenticated.");
        }

        if (!int.TryParse(userIdString, out int userId))
        {
            return BadRequest("Invalid user ID.");
        }

        var comment = await _commentService.AddCommentAsync(postId, commentDto, userId);
        return Ok(comment);
    }



    [HttpDelete("{commentId}")]
    public async Task<ActionResult> RemoveComment(int commentId)
    {
        var comment = await _commentService.RemoveCommentAsync(commentId);
        if (comment == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}
