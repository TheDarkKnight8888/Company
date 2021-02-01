using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Company.DataAccess
{
    internal abstract class EntitySetBase<TEntity> : IEntitySet<TEntity> where TEntity : class
    {
        public Type ElementType => this.Queryable.ElementType;

        public Expression Expression => this.Queryable.Expression;

        public IQueryProvider Provider => this.Queryable.Provider;

        public abstract TEntity Add(TEntity entity);

        public abstract TEntity Remove(TEntity entity);

        public abstract TEntity Update(TEntity entity);

        protected abstract IQueryable<TEntity> Queryable { get; }

        public abstract int SaveChanges();

        public IEnumerator<TEntity> GetEnumerator() => this.Queryable.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => (this.Queryable as IEnumerable).GetEnumerator();
    }
}
