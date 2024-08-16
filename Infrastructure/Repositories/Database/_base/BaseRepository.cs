using Infrastructure.DTOs.Database;
using SQLite;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Database._base
{
    internal class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseTableDB, new()
    {
        protected SQLiteConnection _connection;

        public BaseRepository(SQLiteConnection connection)
        {
            _connection = connection;
            _connection.CreateTable<TEntity>();
        }
        public bool DeleteItem(TEntity item)
        {
            int result = _connection.Delete(item);
            return result > 0;
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public TEntity GetItem(int id)
        {
            return _connection.Table<TEntity>()
                  .FirstOrDefault(x => x.Id == id);
        }

        public TEntity GetItem(Expression<Func<TEntity, bool>> predicate)
        {
            return _connection.Table<TEntity>()
                  .Where(predicate)
                  .FirstOrDefault();
        }

        public List<TEntity> GetItems()
        {
            return _connection.Table<TEntity>()
                              .ToList();
        }

        public List<TEntity> GetItems(Expression<Func<TEntity, bool>> predicate)
        {
            return _connection.Table<TEntity>()
                              .Where(predicate)
                              .ToList();
        }



        public bool UpdateItem(TEntity item)
        {
            int result = _connection.Update(item);
            return result > 0;
        }

        public bool SaveItem(TEntity item)
        {
            int result = _connection.Insert(item);
            return result > 0;
        }
    }
}
