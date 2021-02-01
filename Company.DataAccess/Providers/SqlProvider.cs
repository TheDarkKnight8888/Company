using System;
using System.Linq;
using System.Linq.Expressions;

namespace Company.DataAccess
{
    public class SqlProvider<TEntity> : IQueryProvider where TEntity : class
    {
        private readonly DbSet<TEntity> dbSet;

        public SqlProvider(IQueryable<TEntity> dbSet)
        {
            this.dbSet = (DbSet<TEntity>)dbSet;
        }

        internal MemberType SelectMember 
        { 
            get; 
            set; 
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return this.dbSet;
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            this.dbSet.Expression = expression;
            return (IQueryable<TElement>)this.dbSet;
        }

        public object Execute(Expression expression)
        {
            return Execute<TEntity>(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            Type resultType = typeof(TResult);
            var isEnumerable = (resultType.Name == "IEnumerable`1");
            var converter = new ExpressionTreeConverter<TResult>();
            string members = this.GetMembers();
            string predicate = converter.ConvertToSqlPredicate(expression, isEnumerable);

            return (TResult)this.dbSet.ExecuteQuery(members, predicate, isEnumerable, converter.SplParameters);
        }

        private string GetMembers()
        {
            string result = null;
            if (SelectMember == MemberType.First)
            {
                result = "TOP 1 *";
            }
            else if (SelectMember == MemberType.All)
            {
                result = "*";
            }

            return result;
        }
    }
}
