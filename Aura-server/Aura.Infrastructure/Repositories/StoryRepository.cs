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

    // Create a new story
    public async Task<Story> CreateStoryAsync(Story story)
    {
        _context.Stories.Add(story);
        await _context.SaveChangesAsync();
        return story;
    }

    public async Task<List<Story>> GetStoriesAsync()
    {
        return await _context.Stories
            .Include(s => s.Image) 
            .Where(s => !s.IsDeleted) 
            .ToListAsync();
    }

    public async Task DeleteExpiredStoriesAsync(DateTime expirationTime)
    {
        var expiredStories = await _context.Stories
            .Where(s => s.DateCreated < expirationTime && !s.IsDeleted)
            .ToListAsync();

        foreach (var story in expiredStories)
        {
            story.IsDeleted = true;
        }

        await _context.SaveChangesAsync();
    }
}