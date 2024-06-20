namespace Bleeter.BleetsService.Dtos;

public class GetCommentDto
{
    public Guid BleetId { get; set; }
    public GetBleetAuthorDto Author { get; set; }
    public string Content { get; set; }
    public DateTime DateCreatedUtc { get; set; }
    public DateTime? DateModifiedUtc { get; set; }
}