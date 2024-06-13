using Bleeter.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace Bleeter.BleetsService.Data;

public class BleetsContext : BaseDbContext<BleetsContext>
{
    public BleetsContext(DbContextOptions<BleetsContext> options) : base(options)
    {
    }
}