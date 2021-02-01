using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Expressions = Company.DataAccess.Extensions.QueryableExtensions;

namespace Company.DataAccess
{
    public static class QueryableExtensions
    {
        public static TEntity FirstOrDefaultItem<TEntity>(this IQueryable<TEntity> source, Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            if (source is IEntitySet<TEntity>)
            {
                source = source as IEntitySet<TEntity>;
            }

            return Expressions.FirstOrDefaultItem<TEntity>(source, predicate);
        }

        public static IEnumerable<TEntity> WhereItem<TEntity>(this IQueryable<TEntity> source, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return Expressions.WhereItem<TEntity>(source, predicate);
        }
    }
}
