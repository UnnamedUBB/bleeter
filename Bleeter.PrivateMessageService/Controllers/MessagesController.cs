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
    public ActionResult<IEnumerable<MessageLog>> GetUserMessages(string user)
    {
        var logs = ChatHub.GetMessageLogs().Where(log => log.ToUser == user);
        return Ok(logs);
    }

   
}

public class MessageDto
{
    public string ToUser { get; set; }
    public string Content { get; set; }
}
