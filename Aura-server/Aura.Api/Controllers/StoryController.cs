using Aura.Application.Abstracts;
using Aura.Application.Services;
using Aura.Domain.DTOs.Story;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult<StoryResponseDto>> CreateStory(CreateStoryDto createStoryDto)
    {
        var story = await _storyService.CreateStoryAsync(createStoryDto);
        return Ok(new StoryResponseDto
        {
            Id = story.Id,
            ImagePath = story.ImagePath,
            DateCreated = story.DateCreated,
            IsDeleted = story.IsDeleted
        });
    }

    [HttpGet]
    public async Task<ActionResult<List<StoryResponseDto>>> GetStories()
    {
        var stories = await _storyService.GetStoriesAsync();
        return Ok(stories);
    }

    [HttpDelete("delete-expired")]
    public async Task<ActionResult> DeleteExpiredStories()
    {
        await _storyService.DeleteExpiredStoriesAsync();
        return NoContent();
    }
}