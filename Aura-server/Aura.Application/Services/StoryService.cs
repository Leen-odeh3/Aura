using Aura.Application.Abstracts;
using Aura.Application.Abstracts.FileServices;
using Aura.Application.Abstracts.UserServices;
using Aura.Domain.Contracts;
using Aura.Domain.DTOs.Story;
using Aura.Domain.Entities;

namespace Aura.Application.Services;

public class StoryService : IStoryService
{
    private readonly IStoryRepository _storyRepository;
    private readonly IAuthenticatedUserService _authenticatedUserService;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IFileService _fileService;

    public StoryService(
        IStoryRepository storyRepository,
        IAuthenticatedUserService authenticatedUserService,
        ICloudinaryService cloudinaryService,
        IFileService fileService)
    {
        _storyRepository = storyRepository;
        _authenticatedUserService = authenticatedUserService;
        _cloudinaryService = cloudinaryService;
        _fileService = fileService;
    }

    public async Task<StoryResponseDto> CreateStoryAsync(CreateStoryDto createStoryDto)
    {
        var userId = _authenticatedUserService.GetAuthenticatedUserId();

        if (userId == 0)
        {
            throw new UnauthorizedAccessException("User not authenticated.");
        }

        var story = new Story
        {
            UserId = userId,
            DateCreated = DateTime.UtcNow,
            IsDeleted = false, 
        };

        if (createStoryDto.Image != null)
        {
            var imagePath = await _fileService.StoreImageToLocalFolder(createStoryDto.Image);
            var uploadResults = await _cloudinaryService.UploadImageToCloudinary(imagePath);
            _fileService.DeleteFile(imagePath);

            story.Image = new Image
            {
                ImagePath = uploadResults.Item1,
                CloudinaryIdentifier = uploadResults.Item2
            };
        }

        var createdStory = await _storyRepository.CreateStoryAsync(story);

        return new StoryResponseDto
        {
            Id = createdStory.Id,
            ImagePath = createdStory.Image?.ImagePath,
            DateCreated = createdStory.DateCreated,
            IsDeleted = createdStory.IsDeleted
        };
    }

    public async Task DeleteExpiredStoriesAsync()
    {
        var expirationTime = DateTime.UtcNow.AddHours(-24); // 24 hours ago
        await _storyRepository.DeleteExpiredStoriesAsync(expirationTime);
    }

    public async Task<List<StoryResponseDto>> GetStoriesAsync()
    {
        var stories = await _storyRepository.GetStoriesAsync();
        return stories.Select(s => new StoryResponseDto
        {
            Id = s.Id,
            ImagePath = s.Image?.ImagePath,
            DateCreated = s.DateCreated,
            IsDeleted = s.IsDeleted
        }).ToList();
    }
}

