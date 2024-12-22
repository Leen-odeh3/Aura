namespace Aura.Domain.Entities;
public class Hashtag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }

    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}