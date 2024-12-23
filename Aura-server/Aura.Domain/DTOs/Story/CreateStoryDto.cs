using Microsoft.AspNetCore.Http;
namespace Aura.Domain.DTOs.Story;
public class CreateStoryDto
{
    public IFormFile Image { get; set; }
}