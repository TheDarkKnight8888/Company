using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Company.DataAccess.Extensions
{
    public static class QueryableExtensions
    {
        public static TEntity FirstOrDefaultItem<TEntity>(this IQueryable<TEntity> source, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            TEntity result = default(TEntity);
            var provider = GetProvider<TEntity>(source);
            provider.SelectMember = MemberType.First;
            result = provider.Execute<TEntity>(predicate);

            return result;
        }

        public static IEnumerable<TEntity> WhereItem<TEntity>(this IQueryable<TEntity> source, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            var provider = GetProvider<TEntity>(source);
            provider.SelectMember = MemberType.All;

            return provider.Execute<IEnumerable<TEntity>>(predicate); 
        }

        private static SqlProvider<TEntity> GetProvider<TEntity>(this IQueryable<TEntity> source) where TEntity : class
        {
            var provider = (SqlProvider<TEntity>)source.Provider;
            return provider;
        }
    }
}
