using Aura.Domain.Contracts;
using Aura.Domain.Entities;
using Aura.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Aura.Infrastructure.Repositories;
public class StoryRepository : IStoryRepository
{
    private readonly AppDbContext _context;

    public StoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Story>> GetAllStoriesAsync(int userId)
    {
        return await _context.Stories
            .Where(s => s.UserId == userId && !s.IsDeleted)
            .ToListAsync();
    }

    public async Task<Story> GetStoryByIdAsync(int storyId)
    {
        return await _context.Stories
            .FirstOrDefaultAsync(s => s.Id == storyId && !s.IsDeleted);
    }

    public async Task<Story> CreateStoryAsync(Story story)
    {
        _context.Stories.Add(story);
        await _context.SaveChangesAsync();
        return story;
    }

    public async Task RemoveStoryAsync(int storyId)
    {
        var story = await GetStoryByIdAsync(storyId);
        if (story != null)
        {
            story.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveExpiredStoriesAsync()
    {
        var expiredStories = await _context.Stories
            .Where(s => !s.IsDeleted && s.DateCreated.AddHours(24) <= DateTime.UtcNow)
            .ToListAsync();

        foreach (var story in expiredStories)
        {
            story.IsDeleted = true;
        }
        await _context.SaveChangesAsync();
    }
}