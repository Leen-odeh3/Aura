using AutoMapper;
using Aura.Domain.Contracts;
using Aura.Domain.Entities;
using Aura.Domain.DTOs.Comment;
using Aura.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Aura.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CommentRepository(AppDbContext context, IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<CommentResponseDto>> GetCommentsByPostIdAsync(int postId)
    {
        var comments = await _context.Comments
            .Where(c => c.PostId == postId)
            .Include(c => c.User) 
            .ToListAsync();

        var commentDtos = _mapper.Map<List<CommentResponseDto>>(comments);

        return commentDtos;
    }

    public async Task<Comment> AddCommentAsync(int postId, PostComment postCommentDto, int userId)
    {
        var postExists = await _context.Posts.AnyAsync(p => p.Id == postId);
        if (!postExists)
            throw new ArgumentException("The specified PostId does not exist.");

        var comment = _mapper.Map<Comment>(postCommentDto);

        comment.PostId = postId;
        comment.UserId = userId; 
        comment.DateCreated = DateTime.UtcNow;
        comment.DateUpdated = DateTime.UtcNow;

        _context.Comments.Add(comment);
        await _unitOfWork.SaveChangesAsync();

        return comment;
    }

    public async Task<Comment> RemoveCommentAsync(int commentId)
    {
        var comment = await _context.Comments.FindAsync(commentId);
        if (comment != null)
        {
            _context.Comments.Remove(comment);
            await _unitOfWork.SaveChangesAsync();
        }
        return comment;
    }
}
