
namespace Aura.Domain.DTOs.Comment;
public class CommentResponseDto
{
    public string Content { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public string Username { get; set; }  
}