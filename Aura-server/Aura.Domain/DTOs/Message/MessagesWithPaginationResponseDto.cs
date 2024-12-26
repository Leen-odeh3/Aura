namespace Aura.Domain.DTOs.Message;
public class MessagesWithPaginationResponseDto
{
    public IEnumerable<MessageResponseDto> Messages { get; set; }
    public bool IsThereMore { get; set; }
}
