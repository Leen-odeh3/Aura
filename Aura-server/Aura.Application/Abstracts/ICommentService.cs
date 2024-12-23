using Aura.Domain.DTOs.Comment;
using Aura.Domain.Entities;
namespace Aura.Application.Abstracts;
public interface ICommentService
{
    Task<List<CommentResponseDto>> GetCommentsByPostIdAsync(int postId);
    Task<Comment> AddCommentAsync(int postId, PostComment comment, int userId);
    Task<Comment> RemoveCommentAsync(int commentId);
}
