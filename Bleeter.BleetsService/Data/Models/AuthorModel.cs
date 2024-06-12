using Bleeter.Shared.Data;
using Bleeter.Shared.Data.Interfaces;

namespace Bleeter.BleetsService.Data.Models;

public class AuthorModel : BaseModel, IAuditable
{
    public Guid AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<BleetModel> Bleets { get; set; }
    public ICollection<CommentModel> Comments { get; set; }
    public ICollection<LikeModel> Likes { get; set; }
    
    public DateTime DateCreatedUtc { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? DateModifiedUtc { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? DataDeletedUtc { get; set; }
    public Guid? DateDeletedUtc { get; set; }
}