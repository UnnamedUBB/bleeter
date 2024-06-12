using Bleeter.BleetsService.Data.Models;
using Bleeter.BleetsService.Data.Repositories.Interfaces;
using Bleeter.Shared.Data.Repositories;

namespace Bleeter.BleetsService.Data.Repositories;

public class LikeRepository : BaseAuditableRepository<BleetsContext, LikeModel>, ILikeRepository
{
    public LikeRepository(BleetsContext context) : base(context)
    {
    }
}