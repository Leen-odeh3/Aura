using AutoMapper;
using Aura.Domain.Contracts;
using Aura.Domain.Entities;
using Aura.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Aura.Domain.DTOs.Post;

namespace Aura.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PostRepository(AppDbContext context, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<PostResponseDto>> GetAllPostsAsync(int loggedInUserId)
    {
        var posts = await _context.Posts
            .Where(p => p.UserId == loggedInUserId || !p.IsPrivate)
            .Include(p => p.User)
            .Include(p => p.Likes)
            .Include(p => p.Comments)
            .Include(p => p.Favorites)
            .Include(p => p.Reposts).Include(p => p.Image)
            .ToListAsync();

        return _mapper.Map<List<PostResponseDto>>(posts);
    }

    public async Task<Post> GetPostByIdAsync(int postId)
    {
        return await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Likes)
            .Include(p => p.Comments)
            .Include(p => p.Favorites)
            .Include(p => p.Reposts).Include(p => p.Image)
            .FirstOrDefaultAsync(p => p.Id == postId);
    }

    public async Task<List<Post>> GetAllFavoritedPostsAsync(int loggedInUserId)
    {
        return await _context.Favorites
            .Where(f => f.UserId == loggedInUserId)
            .Select(f => f.Post)
            .ToListAsync();
    }

    public async Task<Post> CreatePostAsync(Post post)
    {
        _context.Posts.Add(post);
        await _unitOfWork.SaveChangesAsync();
        return post;
    }

    public async Task<Post> RemovePostAsync(int postId)
    {
        var post = await GetPostByIdAsync(postId);
        if (post != null)
        {
            _context.Posts.Remove(post);
            await _unitOfWork.SaveChangesAsync();
        }
        return post;
    }
}
