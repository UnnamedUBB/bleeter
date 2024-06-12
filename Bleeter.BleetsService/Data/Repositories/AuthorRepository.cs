using Bleeter.BleetsService.Data.Models;
using Bleeter.BleetsService.Data.Repositories.Interfaces;
using Bleeter.Shared.Data.Repositories;

namespace Bleeter.BleetsService.Data.Repositories;

public class AuthorRepository : BaseAuditableRepository<BleetsContext, AuthorModel>, IAuthorRepository
{
    public AuthorRepository(BleetsContext context) : base(context)
    {
    }
}