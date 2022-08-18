using System.Linq.Expressions;

namespace Domain.AccountMicroDomain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> Get(int id);
        Task<List<TEntity>> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

       void Add(TEntity entity);

        void Remove(TEntity entity);



    }
}
