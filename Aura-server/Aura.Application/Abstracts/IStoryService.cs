using Aura.Domain.DTOs.Story;
using Aura.Domain.Entities;

namespace Aura.Application.Abstracts;
public interface IStoryService
{
    Task<Story> CreateStoryAsync(CreateStoryDto storyCreateDto);
    Task<List<StoryResponseDto>> GetAllStoriesAsync(int userId);
    Task<Story> GetStoryByIdAsync(int storyId);
    Task RemoveStoryAsync(int storyId);
    Task RemoveExpiredStoriesAsync();
}