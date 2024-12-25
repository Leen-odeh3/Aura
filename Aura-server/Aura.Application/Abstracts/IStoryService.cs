using Aura.Domain.DTOs.Story;

namespace Aura.Application.Abstracts;
public interface IStoryService
{
    Task<StoryResponseDto> CreateStoryAsync(CreateStoryDto createStoryDto);
    Task DeleteExpiredStoriesAsync();
    Task<List<StoryResponseDto>> GetStoriesAsync();
}