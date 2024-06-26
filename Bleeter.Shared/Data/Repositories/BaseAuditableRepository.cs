using Bleeter.Shared.Data.Interfaces;
using Bleeter.Shared.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bleeter.Shared.Data.Repositories
{
    public abstract class BaseAuditableRepository<TContext, TModel> : BaseRepository<TContext, TModel>, IBaseAuditableRepository<TContext, TModel> 
        where TContext : DbContext
        where TModel : class, IAuditable
    {
        public BaseAuditableRepository(TContext context) : base(context)
        {
        }

        public override void Add(TModel model)
        {
            model.DateCreatedUtc = DateTime.UtcNow;
            base.Add(model);
        }

        public override void AddRange(IEnumerable<TModel> models)
        {
            foreach (var auditable in models)
            {
                auditable.DateCreatedUtc = DateTime.UtcNow;
            }

            base.AddRange(models);
        }

        public override void Update(TModel model)
        {
            model.DateModifiedUtc = DateTime.UtcNow;
            base.Update(model);
        }

        public override void Delete(TModel model)
        {
            model.DataDeletedUtc = DateTime.UtcNow;
            base.Delete(model);
        }
    }
}