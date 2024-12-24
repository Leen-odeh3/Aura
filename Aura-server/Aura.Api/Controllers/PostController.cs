using Aura.Application.Abstracts;
using Aura.Domain.DTOs.Post;
using Aura.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aura.Api.Controllers;
[Route("api/posts")]
[ApiController]
[Authorize]

public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]

    public async Task<ActionResult<List<Post>>> GetAllPosts(int loggedInUserId)
    {
        var posts = await _postService.GetAllPostsAsync(loggedInUserId);
        return Ok(posts);
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost(CreatePost post)
    {
        var newPost = await _postService.CreatePostAsync(post);
        return Ok(newPost);
    }

    [HttpDelete("{postId}")]
    public async Task<ActionResult> RemovePost(int postId)
    {
        var post = await _postService.RemovePostAsync(postId);
        if (post is null)
        {
            return NotFound();
        }
        return NoContent();
    }
}
