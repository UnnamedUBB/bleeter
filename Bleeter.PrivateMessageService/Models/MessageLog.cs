namespace Bleeter.PrivateMessageService.Models;

public class MessageLog
{
    public string ToUser { get; set; }
    public string FromUser { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
}

