using System.Linq.Expressions;
using Bleeter.Shared.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bleeter.Shared.Data.Repositories.Interfaces;

public interface IBaseAuditableRepository<TContext, TModel> : IBaseRepository<TContext, TModel> 
    where TContext : DbContext 
    where TModel : class, IAuditable
{
}