using Bleeter.PrivateMessageService.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    private static ConcurrentBag<MessageLog> _messageLogs = new ConcurrentBag<MessageLog>();

    public async Task SendMessageToUser(string user, string message)
    {
        var fromUser = Context.User.Identity.Name;
        var log = new MessageLog
        {
            ToUser = user,
            FromUser = fromUser,
            Content = message,
            Timestamp = DateTime.UtcNow
        };
        _messageLogs.Add(log);
        Console.WriteLine($"Message from {fromUser} to {user}: {message} : {log}");

        await Clients.User(user).SendAsync("ReceiveMessage", message);
    }

    public override async Task OnConnectedAsync()
    {
        var userName = Context.User.Identity.Name;
        await Groups.AddToGroupAsync(Context.ConnectionId, userName);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userName = Context.User.Identity.Name;
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, userName);
        await base.OnDisconnectedAsync(exception);
    }

    public static ConcurrentBag<MessageLog> GetMessageLogs() => _messageLogs;
}
