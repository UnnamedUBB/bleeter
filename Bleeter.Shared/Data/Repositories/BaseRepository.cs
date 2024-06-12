using System.Linq.Expressions;
using Bleeter.Shared.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bleeter.Shared.Data.Repositories;

public abstract class BaseRepository<TContext, TModel> : IBaseRepository<TContext, TModel>
    where TContext : BaseDbContext
    where TModel : class
{
    private readonly TContext _context;

    public BaseRepository(TContext context)
    {
        _context = context;
    }

    public Task<TModel?> GetAsync(Expression<Func<TModel, bool>> expression = null,
        Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
        params Expression<Func<TModel, object>>[] includes)
    {
        return PrepareQueryable(expression, orderBy, includes).FirstOrDefaultAsync();
    }

    public Task<List<TModel>> GetAllAsync(Expression<Func<TModel, bool>> expression = null,
        Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
        params Expression<Func<TModel, object>>[] includes)
    {
        return PrepareQueryable(expression, orderBy, includes).ToListAsync();
    }

    public async Task<PageableList<TModel>> GetAllWithPaginationAsync(Expression<Func<TModel, bool>> expression = null,
        Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
        int? page = null,
        int? pageSize = null,
        params Expression<Func<TModel, object>>[] includes)
    {
        var query = PrepareQueryable(expression, orderBy, includes);

        var total = await query.CountAsync();

        if (page is not null && pageSize is not null)
        {
            var preparedPage = page.Value - 1 < 0 ? 0 : page.Value - 1;
            query = query.Skip(preparedPage * pageSize.Value).Take(pageSize.Value);
        }

        return new PageableList<TModel>
        {
            TotalCount = total,
            Data = await query.ToListAsync()
        };
    }

    public Task<bool> ExistsAsync(Expression<Func<TModel, bool>> expression)
    {
        return _context.Set<TModel>().AnyAsync(expression);
    }

    public virtual void Add(TModel model)
    {
        _context.Add(model);
    }

    public virtual void AddRange(IEnumerable<TModel> models)
    {
        _context.AddRange(models);
    }

    public virtual void Update(TModel model)
    {
        _context.Update(model);
    }

    public virtual void Delete(TModel model)
    {
        _context.Remove(model);
    }

    public Task<int> SaveAsync()
    {
        return _context.SaveChangesAsync();
    }

    protected virtual IQueryable<TModel> PrepareQueryable(Expression<Func<TModel, bool>> expression = null,
        Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
        params Expression<Func<TModel, object>>[] includes)
    {
        var baseQuery = _context.Set<TModel>()
            .AsNoTracking()
            .AsQueryable();

        if (expression is not null)
        {
            baseQuery = baseQuery.Where(expression);
        }

        if (orderBy is not null)
        {
            baseQuery = orderBy(baseQuery);
        }

        baseQuery = includes.Aggregate(baseQuery, (curr, acc) => curr.Include(acc));

        return baseQuery;
    }
}