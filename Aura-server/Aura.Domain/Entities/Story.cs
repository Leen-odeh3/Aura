namespace Aura.Domain.Entities;
public class Story
{
    public int Id { get; set; }
    public int? ImageId { get; set; }
    public Image Image { get; set; }
    public DateTime DateCreated { get; set; }
    public bool IsDeleted { get; set; }

    // Foreign key
    public int UserId { get; set; }

    //Navigation properties
    public User User { get; set; }
}
