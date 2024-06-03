using Bleeter.PrivateMessageService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IHubContext<ChatHub> _hubContext;

    public MessagesController(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] MessageDto message)
    {
        await _hubContext.Clients.User(message.ToUser).SendAsync("ReceiveMessage", message.Content);
        return Ok();
    }

    [HttpGet("logs/{user}")]
    public ActionResult<IEnumerable<MessageLog>> GetUserMessages(Guid user)
    {
        var logs = ChatHub.GetMessageLogs().Where(log => log.TargetId == user);
        return Ok(logs);
    }
    [HttpGet("logsS/{user}")]
    public ActionResult<IEnumerable<MessageLog>> GetUserMessagesS(Guid user)
    {
        ChatHub chatHub = new ChatHub();
        var logs = chatHub.SendMessageToUser(user, "test");
        return Ok(logs);
    }

    [HttpGet("allLogs")]
    public ActionResult<IEnumerable<MessageLog>> GetAllMessages()
    {
        var logs = ChatHub.GetMessageLogs();
        return Ok(logs);
    }

}

public class MessageDto
{
    public string ToUser { get; set; }
    public string Content { get; set; }
}
