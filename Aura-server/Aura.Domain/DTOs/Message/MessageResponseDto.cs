﻿
namespace Aura.Domain.DTOs.Message;
public class MessageResponseDto
{
    public int Id { get; set; }
    public string TextBody { get; set; }
    public DateTime CreationDate { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
}