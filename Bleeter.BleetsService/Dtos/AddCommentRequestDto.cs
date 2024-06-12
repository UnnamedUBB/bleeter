namespace Bleeter.BleetsService.Dtos;

public class AddCommentRequestDto
{
    public Guid BleetId { get; set; }
    public string Content { get; set; }
}