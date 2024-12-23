using Microsoft.AspNetCore.Http;

namespace Aura.Domain.DTOs.Post;
public class CreatePost
{
    public string Content { get; set; }
    public bool IsPrivate { get; set; }
    public IFormFile? Image { get; set; }
}
