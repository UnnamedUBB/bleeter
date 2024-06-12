namespace Bleeter.Shared.Data.Interfaces;

public interface IModified
{
    public DateTime? DateModifiedUtc { get; set; }
    public Guid? ModifiedBy { get; set; }
}