namespace Bleeter.Shared.DAL.Interfaces;

public interface ICreated
{
    public DateTime DateCreatedUtc { get; set; }
    public Guid? CreatedBy { get; set; }
}