using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    private static ConcurrentBag<MessageLog> _messageLogs = new ConcurrentBag<MessageLog>();

    public async Task SendMessageToUser(string user, string message)
    {
        var fromUser = Context.User?.Identity?.Name ?? "Anonymous";
        var log = new MessageLog
        {
            ToUser = user,
            FromUser = fromUser,
            Content = message,
            Timestamp = DateTime.UtcNow
        };
        _messageLogs.Add(log);

        // Wypisywanie wiadomości na konsolę
        Console.WriteLine($"Message from {fromUser} to {user}: {message}");

        await Clients.User(user).SendAsync("ReceiveMessage", message);
    }

    public static ConcurrentBag<MessageLog> GetMessageLogs() => _messageLogs;
}

public class MessageLog
{
    public string ToUser { get; set; }
    public string FromUser { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
}
