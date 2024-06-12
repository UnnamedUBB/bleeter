using Bleeter.BleetsService.Data.Models;
using Bleeter.BleetsService.Data.Repositories.Interfaces;
using Bleeter.Shared.Data.Repositories;

namespace Bleeter.BleetsService.Data.Repositories;

public class BleetRepository : BaseAuditableRepository<BleetsContext, BleetModel>, IBleetRepository
{
    public BleetRepository(BleetsContext context) : base(context)
    {
    }
}