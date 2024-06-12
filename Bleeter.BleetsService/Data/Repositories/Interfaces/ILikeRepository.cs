using Bleeter.BleetsService.Data.Models;
using Bleeter.Shared.Data.Repositories.Interfaces;

namespace Bleeter.BleetsService.Data.Repositories.Interfaces;

public interface ILikeRepository : IBaseAuditableRepository<BleetsContext, LikeModel>
{
}