using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Bleeter.Shared.Data.Repositories.Interfaces;

public interface IBaseRepository<TContext, TModel> 
    where TContext : DbContext
    where TModel : class
{
    public Task<TModel?> GetAsync(Expression<Func<TModel, bool>> expression = null,
        Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
        params Expression<Func<TModel, object>>[] includes);

    public Task<List<TModel>> GetAllAsync(Expression<Func<TModel, bool>> expression = null,
        Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
        params Expression<Func<TModel, object>>[] includes);

    public Task<PageableList<TModel>> GetAllWithPaginationAsync(Expression<Func<TModel, bool>> expression = null,
        Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
        int? page = null,
        int? pageSize = null,
        params Expression<Func<TModel, object>>[] includes);
    
    public Task<PageableList<T>> GetAllWithPaginationAsync<T>(Expression<Func<TModel, bool>> expression = null,
        Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
        int? page = null,
        int? pageSize = null,
        params Expression<Func<TModel, object>>[] includes);

    public Task<bool> ExistsAsync(Expression<Func<TModel, bool>> expression);

    public void Add(TModel model);
    public void AddRange(IEnumerable<TModel> models);
    public void Update(TModel model);
    public void Delete(TModel model);
    public Task<int> SaveAsync();
}