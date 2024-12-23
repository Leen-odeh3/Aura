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
    public ICollection<PrivateMessage> SendedPrivateMessages { get; set; }
    public ICollection<PrivateMessage> ReceivedPrivateMessages { get; set; }
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Story> Stories { get; set; } = new List<Story>();
    public ICollection<Like> Likes { get; set; } = new List<Like>();
    [JsonIgnore]
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    public ICollection<Repost> Reposts { get; set; } = new List<Repost>();
}