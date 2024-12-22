namespace Aura.Domain.Entities;
public class Repost
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }

    public int PostId { get; set; }
    public int UserId { get; set; }

    // Navigation properties
    public Post Post { get; set; }
    public User User { get; set; }
}
