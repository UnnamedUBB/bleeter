namespace Bleeter.Shared.Messages;

public class AccountUpdatedMessage
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
}