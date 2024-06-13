namespace Bleeter.Shared.Messages;

public class EmailConfirmationMessage
{
    public string Email { get; set; }
    public string ConfirmationLink { get; set; }
}