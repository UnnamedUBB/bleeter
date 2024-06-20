using System.ComponentModel.DataAnnotations.Schema;
using Bleeter.Shared.Data;
using Bleeter.Shared.Data.Interfaces;

namespace Bleeter.BleetsService.Data.Models;

[Table("Authors")]
public class AuthorModel : BaseModel, IAuditable
{
    public string UserName { get; set; }

    public virtual ICollection<BleetModel> Bleets { get; set; }
    public virtual ICollection<CommentModel> Comments { get; set; }
    public virtual ICollection<LikeModel> Likes { get; set; }
    
    public DateTime DateCreatedUtc { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? DateModifiedUtc { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? DataDeletedUtc { get; set; }
    public Guid? DeletedBy { get; set; }
}