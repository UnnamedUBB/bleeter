using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Bleeter.PrivateMessageService.Models;

public class ChatHub : Hub
{
    private static ConcurrentBag<MessageLog> _messageLogs = new ConcurrentBag<MessageLog>();

    public async Task SendMessageToUser(Guid user, string message)
    {
        Guid fromUser = new Guid("00000000-0000-0000-0000-000000000001");
        var log = new MessageLog
        {
            TargetId = user,
            AuthorId = fromUser,
            Content = message,
            Timestamp = DateTime.UtcNow
        };
        _messageLogs.Add(log);

        // Wypisywanie wiadomości na konsolę
        Console.WriteLine($"Message from {fromUser} to {user}: {message}");

       
       
       // await Clients.User(user.ToString()).SendAsync("ReceiveMessage", message);
    }

    public static ConcurrentBag<MessageLog> GetMessageLogs() => _messageLogs;
}


