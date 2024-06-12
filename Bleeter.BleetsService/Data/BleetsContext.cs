using Bleeter.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace Bleeter.BleetsService.Data;

public class BleetsContext : BaseDbContext
{
    public BleetsContext(DbContextOptions options) : base(options) {}
}