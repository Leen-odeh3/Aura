﻿namespace Aura.Domain.Entities;
public class Message
{
    public int Id { get; set; }
    public string TextBody { get; set; }
    public DateTime CreationDate { get; set; }
    public int SenderId { get; set; }
    public User Sender { get; set; }
    public int ReceiverId { get; set; }
    public User Receiver { get; set; }
}