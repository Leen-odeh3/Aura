namespace Aura.Domain.Entities;
public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }

    //Foreign keys
    public int PostId { get; set; }
    public int UserId { get; set; }

    // Navigation properties
    public Post Post { get; set; }
    public User User { get; set; }
}
