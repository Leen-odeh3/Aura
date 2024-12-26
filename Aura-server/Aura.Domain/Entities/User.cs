using System.Text.Json.Serialization;

namespace Aura.Domain.Entities;
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string About { get; set; } = string.Empty;
    public int? ImageId { get; set; }
    public Image Image { get; set; }
    public byte[] PasswordHash { get; set; } = new byte[32];
    public byte[] PasswordSalt { get; set; } = new byte[32];
    public ICollection<Follow> Followers { get; set; } = new List<Follow>();
    public ICollection<Follow> Following { get; set; } = new List<Follow>();
    public ICollection<Message> SendedPrivateMessages { get; set; }
    public ICollection<Message> ReceivedPrivateMessages { get; set; }
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Story> Stories { get; set; } = new List<Story>();
    public ICollection<Like> Likes { get; set; } = new List<Like>();
    [JsonIgnore]
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    public ICollection<Repost> Reposts { get; set; } = new List<Repost>();
}