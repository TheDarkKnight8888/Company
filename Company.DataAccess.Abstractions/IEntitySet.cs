using System.Linq;

namespace Company.DataAccess
{
    public interface IEntitySet<TEntity> : IQueryable<TEntity>, IEntityStorage where TEntity : class
    {
        TEntity Add(TEntity entity);

        TEntity Remove(TEntity entity);

        TEntity Update(TEntity entity);
    }
}
