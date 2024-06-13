using System.ComponentModel.DataAnnotations;

namespace Bleeter.Shared.Data;

public abstract class BaseModel 
{
    [Key]
    public Guid Id { get; set; }
}