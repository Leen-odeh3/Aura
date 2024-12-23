using Aura.Application.Abstracts;
using Aura.Application.Services;
using Aura.Domain.DTOs.Story;
using Aura.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aura.Api.Controllers;
[Route("api/stories")]
[ApiController]
[Authorize]
public class StoryController : ControllerBase
{
    private readonly IStoryService _storyService;

    public StoryController(IStoryService storyService)
    {
        _storyService = storyService;
    }

    [HttpPost]
    public async Task<ActionResult<Story>> CreateStory([FromForm] CreateStoryDto createStoryDto)
    {
        var newStory = await _storyService.CreateStoryAsync(createStoryDto);
        return CreatedAtAction(nameof(GetStoryById), new { storyId = newStory.Id }, newStory);
    }

    [HttpGet("{storyId}")]
    public async Task<ActionResult<StoryResponseDto>> GetStoryById(int storyId)
    {
        var story = await _storyService.GetStoryByIdAsync(storyId);
        if (story == null)
        {
            return NotFound();
        }
        return Ok(story);
    }

    [HttpGet]
    public async Task<ActionResult<List<StoryResponseDto>>> GetAllStories(int userId)
    {
        var stories = await _storyService.GetAllStoriesAsync(userId);
        return Ok(stories);
    }

    [HttpDelete("{storyId}")]
    public async Task<ActionResult> RemoveStory(int storyId)
    {
        await _storyService.RemoveStoryAsync(storyId);
        return NoContent();
    }
}