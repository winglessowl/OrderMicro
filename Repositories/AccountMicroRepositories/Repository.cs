using Domain.AccountMicroDomain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories.AccountMicroRepositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext context;
        public Repository(DbContext context)
        {
            this.context = context;
        }
        public void Add(TEntity entity)
        {
            context.Add<TEntity>(entity);
        }

        public void AddRange(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate);
        }

        public Task<TEntity?> Get(int id)
        {
            return context.Set<TEntity?>().FindAsync(id).AsTask();
        }


        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        Task<List<TEntity>> IRepository<TEntity>.GetAll()
        {
            return context.Set<TEntity>().ToListAsync();
        }
    }
}
