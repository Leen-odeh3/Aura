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

    [HttpGet("{postId}")]
    public async Task<ActionResult<PostResponseDto>> GetPostById(int postId)
    {
        var post = await _postService.GetPostByIdAsync(postId);
        if (post == null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost(CreatePost post)
    {
        var newPost = await _postService.CreatePostAsync(post);
        return CreatedAtAction(nameof(GetPostById), new { postId = newPost.Id }, newPost);
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
