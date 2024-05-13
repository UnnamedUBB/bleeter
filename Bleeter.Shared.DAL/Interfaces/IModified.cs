namespace Bleeter.Shared.DAL.Interfaces;

public interface IModified
{
    public DateTime? DateModifiedUtc { get; set; }
    public Guid? ModifiedBy { get; set; }
}