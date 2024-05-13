using Bleeter.Shared.DAL;
using Microsoft.EntityFrameworkCore;

namespace Bleeter.BleetService.DAL;

public class BleetContext : BaseDbContext
{
    public BleetContext(DbContextOptions options) : base(options) {}
}