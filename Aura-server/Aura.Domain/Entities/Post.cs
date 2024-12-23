using System.ComponentModel.DataAnnotations;

namespace Aura.Domain.Entities;
public class Post
{
    [Key]
    public int Id { get; set; }
    public string Content { get; set; }
    public int? ImageId { get; set; }
    public Image Image { get; set; }
    public int NrOfReposts { get; set; }
    public bool IsPrivate { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }

    // Foreign key
    public int UserId { get; set; }

    //Navigation properties
    public User User { get; set; }
    public ICollection<Like> Likes { get; set; } = new List<Like>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    public ICollection<Repost> Reposts { get; set; } = new List<Repost>();

}