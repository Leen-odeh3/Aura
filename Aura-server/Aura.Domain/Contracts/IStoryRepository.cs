using Aura.Domain.Entities;

namespace Aura.Domain.Contracts;
public interface IStoryRepository
{
    Task<Story> GetStoryByIdAsync(int storyId);
    Task<List<Story>> GetAllStoriesAsync(int userId);
    Task<Story> CreateStoryAsync(Story story);
    Task RemoveStoryAsync(int storyId);
    Task RemoveExpiredStoriesAsync();
}