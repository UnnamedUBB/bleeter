namespace Bleeter.PrivateMessageService.Models;

public class MessageLog
{
    public Guid Id { get; set; }
    public Guid TargetId { get; set; }
    public Guid AuthorId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
}

