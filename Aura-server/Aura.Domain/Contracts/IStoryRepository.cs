using Aura.Domain.Entities;
namespace Aura.Domain.Contracts;
public interface IStoryRepository
{
    Task<Story> CreateStoryAsync(Story story);
    Task<List<Story>> GetStoriesAsync();
    Task DeleteExpiredStoriesAsync(DateTime expirationTime);
}