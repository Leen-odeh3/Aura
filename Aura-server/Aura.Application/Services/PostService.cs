using Aura.Application.Abstracts;
using Aura.Application.Abstracts.FileServices;
using Aura.Application.Abstracts.UserServices;
using Aura.Domain.Contracts;
using Aura.Domain.DTOs.Post;
using Aura.Domain.Entities;

namespace Aura.Application.Services;

public class PostService : IPostService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IFileService _fileService;
    private readonly IAuthenticatedUserService _authenticatedUserService;
    private readonly IPostRepository _postRepository;

    public PostService(
        IUnitOfWork unitOfWork,
        ICloudinaryService cloudinaryService,
        IFileService fileService,
        IAuthenticatedUserService authenticatedUserService,
        IPostRepository postRepository)
    {
        _unitOfWork = unitOfWork;
        _cloudinaryService = cloudinaryService;
        _fileService = fileService;
        _authenticatedUserService = authenticatedUserService;
        _postRepository = postRepository;
    }

    public async Task<Post> CreatePostAsync(CreatePost postCreateDto)
    {
        var authenticatedUserId = _authenticatedUserService.GetAuthenticatedUserId();

        var post = new Post
        {
            Content = postCreateDto.Content,
            IsPrivate = postCreateDto.IsPrivate,
            UserId = authenticatedUserId
        };

        // Handle image upload if available
        if (postCreateDto.Image != null)
        {
            var imageLocalPath = await _fileService.StoreImageToLocalFolder(postCreateDto.Image);
            var uploadResults = await _cloudinaryService.UploadImageToCloudinary(imageLocalPath);
            _fileService.DeleteFile(imageLocalPath);

            post.Image = new Image
            {
                ImagePath = uploadResults.Item1,
                CloudinaryIdentifier = uploadResults.Item2
            };
        }

        await _postRepository.CreatePostAsync(post);
        await _unitOfWork.SaveChangesAsync();

        return post;
    }

    public async Task<List<PostResponseDto>> GetAllPostsAsync(int loggedInUserId)
    {
        return await _postRepository.GetAllPostsAsync(loggedInUserId);
    }

    public async Task<Post> GetPostByIdAsync(int postId)
    {
        return await _postRepository.GetPostByIdAsync(postId);
    }

    public async Task<List<Post>> GetAllFavoritedPostsAsync(int loggedInUserId)
    {
        return await _postRepository.GetAllFavoritedPostsAsync(loggedInUserId);
    }

    public async Task<Post> RemovePostAsync(int postId)
    {
        return await _postRepository.RemovePostAsync(postId);
    }
}
