namespace Bleeter.Shared.DAL.Interfaces;

public interface IDeleted
{
    public DateTime? DataDeletedUtc { get; set; }
    public Guid? DateDeletedUtc { get; set; }
}