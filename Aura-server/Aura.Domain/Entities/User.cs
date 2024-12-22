using System.Xml.Linq;

namespace Aura.Domain.Entities;
public class User
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public bool IsDeleted { get; set; }

    // Navigation properties
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Story> Stories { get; set; } = new List<Story>();
    public ICollection<Like> Likes { get; set; } = new List<Like>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    public ICollection<Repost> Reposts { get; set; } = new List<Repost>();
}