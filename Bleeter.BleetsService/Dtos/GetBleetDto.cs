namespace Bleeter.BleetsService.Dtos;

public class GetBleetDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime DateCreatedUtc { get; set; }
    public DateTime? DateModifiedUtc { get; set; }
    public GetBleetAuthorDto Author { get; set; }
    
    public int Likes { get; set; }
    public int Comments { get; set; }
}