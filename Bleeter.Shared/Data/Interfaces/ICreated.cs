namespace Bleeter.Shared.Data.Interfaces;

public interface ICreated
{
    public DateTime DateCreatedUtc { get; set; }
    public Guid? CreatedBy { get; set; }
}