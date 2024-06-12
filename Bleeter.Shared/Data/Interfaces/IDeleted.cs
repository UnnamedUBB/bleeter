namespace Bleeter.Shared.Data.Interfaces;

public interface IDeleted
{
    public DateTime? DataDeletedUtc { get; set; }
    public Guid? DateDeletedUtc { get; set; }
}