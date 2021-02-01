using System.Linq;

namespace Company.DataAccess
{
    internal sealed class EntitySet<TEntity> : EntitySetBase<TEntity> where TEntity : class
    {
        public EntitySet(DbSet<TEntity> dbSet)
        {
            DbSet = dbSet;
        }

        protected override IQueryable<TEntity> Queryable => DbSet;

        internal DbSet<TEntity> DbSet { get; private set; }

        public override TEntity Add(TEntity entity) => DbSet.Add(entity);

        public override TEntity Remove(TEntity entity) => DbSet.Remove(entity);

        public override int SaveChanges() => DbSet.SaveChanges();

        public override TEntity Update(TEntity entity) => DbSet.Update(entity);
    }
}