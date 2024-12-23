using Aura.Application.Abstracts;
using Aura.Domain.DTOs.Comment;
using Aura.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        var comment = await _commentService.AddCommentAsync(postId, commentDto);
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
