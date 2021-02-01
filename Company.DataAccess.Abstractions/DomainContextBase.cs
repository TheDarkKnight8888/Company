using System;
using System.ComponentModel;
using System.Linq;

namespace Company.DataAccess
{
    internal abstract class DomainContextBase : IDisposable
    {
        private readonly ApplicationDbContext dbContext;
        private bool isDisposed = false;
        private readonly EntitySetCache[] entitySetCache;

        public DomainContextBase(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            this.entitySetCache = this.CreateEntitySetCache(dbContext);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected IEntitySet<TEntity> GetEntitySet<TEntity>() where TEntity : class
        {
            Type key = typeof(TEntity);
            int index = Array.FindIndex(this.entitySetCache, 0, this.entitySetCache.Length, e => e.CacheKey.Equals(key));
            if (index < 0)
            {
                throw new InvalidOperationException();
            }

            var data = this.entitySetCache[index];
            if (data.EntitySet is null)
            {
                var genericType = typeof(EntitySet<>).MakeGenericType(key);
                data.EntitySet = Activator.CreateInstance(genericType, data.DbSet);
            }

            return data.EntitySet as IEntitySet<TEntity>;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (disposing)
            {
                this.dbContext.Dispose();
            }

            this.isDisposed = true;
        }

        private EntitySetCache[] CreateEntitySetCache(ApplicationDbContext dbContext)
        {
            return TypeDescriptor.GetProperties(dbContext).Cast<PropertyDescriptor>()
                            .Where(p => p.PropertyType.IsConstructedGenericType
                            && p.PropertyType.GetGenericTypeDefinition().Equals(typeof(DbSet<>)))
                            .Select(p => new EntitySetCache(p.PropertyType.GenericTypeArguments[0], p.GetValue(dbContext)))
                            .ToArray();
        }

        private struct EntitySetCache
        {
            public EntitySetCache(Type keyType, object dbSet)
            {
                CacheKey = keyType;
                DbSet = dbSet;
                EntitySet = null;
            }

            public Type CacheKey { get; private set; }

            public object DbSet { get; private set; }

            public object EntitySet { get; set; }
        }
    }
}
