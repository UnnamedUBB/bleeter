using Bleeter.BleetsService.Data.Models;
using Bleeter.BleetsService.Data.Repositories.Interfaces;
using Bleeter.Shared.Data.Repositories;

namespace Bleeter.BleetsService.Data.Repositories;

public class CommentRepository : BaseAuditableRepository<BleetsContext, CommentModel>, ICommentRepository
{
    public CommentRepository(BleetsContext context) : base(context)
    {
    }
}