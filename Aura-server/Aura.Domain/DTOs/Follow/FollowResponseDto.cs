namespace Aura.Domain.DTOs.Follow;
public class FollowResponseDto
{
    public int FollowerId { get; set; }
    public int FollowedId { get; set; }
    public DateTime DateFollowed { get; set; }
}