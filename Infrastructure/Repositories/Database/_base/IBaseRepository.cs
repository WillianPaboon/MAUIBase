using System.Linq.Expressions;

namespace Infrastructure.Repositories.Database._base
{
    internal interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        bool SaveItem(TEntity item);

        bool UpdateItem(TEntity item);

        TEntity GetItem(int id);

        TEntity GetItem(Expression<Func<TEntity, bool>> predicate);

        List<TEntity> GetItems();

        List<TEntity> GetItems(Expression<Func<TEntity, bool>> predicate);

        bool DeleteItem(TEntity item);
    }
}
