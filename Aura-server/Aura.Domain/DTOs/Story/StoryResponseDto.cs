namespace Aura.Domain.DTOs.Story;
public class StoryResponseDto
{
    public int Id { get; set; }
    public string ImagePath { get; set; }
    public DateTime DateCreated { get; set; }
    public bool IsDeleted { get; set; }
}
