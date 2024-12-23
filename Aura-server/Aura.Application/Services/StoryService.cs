using Aura.Application.Abstracts;
using Aura.Application.Abstracts.FileServices;
using Aura.Domain.Contracts;
using Aura.Domain.DTOs.Story;
using Aura.Domain.Entities;
namespace Aura.Application.Services;
public class StoryService : IStoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IFileService _fileService;
    private readonly IStoryRepository _storyRepository;

    public StoryService(
        IUnitOfWork unitOfWork,
        ICloudinaryService cloudinaryService,
        IFileService fileService,
        IStoryRepository storyRepository)
    {
        _unitOfWork = unitOfWork;
        _cloudinaryService = cloudinaryService;
        _fileService = fileService;
        _storyRepository = storyRepository;
    }

    public async Task<Story> CreateStoryAsync(CreateStoryDto storyCreateDto)
    {
        var story = new Story
        {
            DateCreated = DateTime.UtcNow,
            IsDeleted = false
        };

        // Handle image upload if available
        if (storyCreateDto.Image != null)
        {
            var imageLocalPath = await _fileService.StoreImageToLocalFolder(storyCreateDto.Image);
            var uploadResults = await _cloudinaryService.UploadImageToCloudinary(imageLocalPath);
            _fileService.DeleteFile(imageLocalPath);

            story.Image = new Image
            {
                ImagePath = uploadResults.Item1,
                CloudinaryIdentifier = uploadResults.Item2
            };
        }

        await _storyRepository.CreateStoryAsync(story);
        await _unitOfWork.SaveChangesAsync();

        return story;
    }

    public async Task<List<StoryResponseDto>> GetAllStoriesAsync(int userId)
    {
        var stories = await _storyRepository.GetAllStoriesAsync(userId);
        return stories.Select(story => new StoryResponseDto
        {
            Id = story.Id,
            ImagePath = story.Image?.ImagePath,
            DateCreated = story.DateCreated,
            IsDeleted = story.IsDeleted
        }).ToList();
    }

    public async Task<Story> GetStoryByIdAsync(int storyId)
    {
        return await _storyRepository.GetStoryByIdAsync(storyId);
    }

    public async Task RemoveStoryAsync(int storyId)
    {
        await _storyRepository.RemoveStoryAsync(storyId);
    }

    public async Task RemoveExpiredStoriesAsync()
    {
        await _storyRepository.RemoveExpiredStoriesAsync();
    }
}