using Aura.Domain.DTOs.Comment;
using Aura.Domain.Entities;

namespace Aura.Domain.Contracts;
public interface ICommentRepository
{
    Task<List<Comment>> GetCommentsByPostIdAsync(int postId);
    Task<Comment> AddCommentAsync(int postId, PostComment comment);
    Task<Comment> RemoveCommentAsync(int commentId);
}
