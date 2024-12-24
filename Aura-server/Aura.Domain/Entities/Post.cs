using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
    public DateTime DateCreated { get; set; } =DateTime.UtcNow;
    public DateTime DateUpdated { get; set; }

    // Foreign key
    public int UserId { get; set; }

    //Navigation properties
    public User User { get; set; }

    [JsonIgnore]
    public ICollection<Like> Likes { get; set; } = new List<Like>();
    [JsonIgnore]
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    [JsonIgnore]
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    [JsonIgnore]
    public ICollection<Repost> Reposts { get; set; } = new List<Repost>();

}