using Aura.Domain.DTOs.Comment;
using Aura.Domain.Entities;

namespace Aura.Domain.Contracts;
public interface ICommentRepository
{
    Task<List<CommentResponseDto>> GetCommentsByPostIdAsync(int postId);
    Task<Comment> AddCommentAsync(int postId, PostComment postCommentDto, int userId);
    Task<Comment> RemoveCommentAsync(int commentId);
}

