using Aura.Domain.DTOs.Post;
using Aura.Domain.Entities;

namespace Aura.Domain.Contracts;
public interface IPostRepository
{
    Task<List<PostResponseDto>> GetAllPostsAsync(int loggedInUserId);
    Task<Post> GetPostByIdAsync(int postId);
    Task<List<Post>> GetAllFavoritedPostsAsync(int loggedInUserId);
    Task<Post> CreatePostAsync(Post post);
    Task<Post> RemovePostAsync(int postId);
}