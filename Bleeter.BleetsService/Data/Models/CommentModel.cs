using Bleeter.Shared.Data;
using Bleeter.Shared.Data.Interfaces;

namespace Bleeter.BleetsService.Data.Models;

public class CommentModel : BaseModel, IAuditable
{
    public Guid AuthorId { get; set; }
    public AuthorModel Author { get; set; }
    
    public Guid BleetId { get; set; }
    public BleetModel Bleet { get; set; }
    
    public string Content { get; set; }
    
    public DateTime DateCreatedUtc { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? DateModifiedUtc { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? DataDeletedUtc { get; set; }
    public Guid? DateDeletedUtc { get; set; }
}