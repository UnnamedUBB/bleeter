using Bleeter.Shared.Data;
using Bleeter.Shared.Data.Interfaces;

namespace Bleeter.BleetsService.Data.Models;

public class LikeModel : BaseModel, IAuditable
{
    public Guid AccountId { get; set; }
    public AuthorModel Account { get; set; }
    
    public Guid BleetId { get; set; }
    public BleetModel Bleet;
    
    public DateTime DateCreatedUtc { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? DateModifiedUtc { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? DataDeletedUtc { get; set; }
    public Guid? DateDeletedUtc { get; set; }
}