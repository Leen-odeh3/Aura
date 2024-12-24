using Aura.Domain.DTOs.Post;
using Aura.Domain.Entities;

namespace Aura.Application.Abstracts;
public interface IPostService
{
    Task<Post> CreatePostAsync(CreatePost postCreateDto);
    Task<List<PostResponseDto>> GetAllPostsAsync(int loggedInUserId);
    Task<List<Post>> GetAllFavoritedPostsAsync(int loggedInUserId);
    Task<Post> RemovePostAsync(int postId);
}