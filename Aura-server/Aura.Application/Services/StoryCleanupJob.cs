
using Aura.Application.Abstracts;

namespace Aura.Application.Services;
public class StoryCleanupJob
{
    private readonly IStoryService _storyService;

    public StoryCleanupJob(IStoryService storyService)
    {
        _storyService = storyService;
    }

    public async Task Execute()
    {
        await _storyService.RemoveExpiredStoriesAsync();
    }
}
