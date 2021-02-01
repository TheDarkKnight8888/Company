using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Company.DataAccess.Iterator
{
    public struct EntityEnumerator<TEntity> : IEnumerator<TEntity> where TEntity : class
    {
        private readonly EnumeratorContext context;

        private int currentIndex;

        private readonly DataRowCollection rows;

        public EntityEnumerator(EnumeratorContext context)
        {
            this.context = context;
            this.currentIndex = context.StartIndex;
            this.rows = context.Table.Rows;
        }
         
        public TEntity Current 
        { 
            get 
            {
                DataRow row = this.rows[this.currentIndex];
                return (TEntity)this.context.MapRowToElement(row); 
            } 
        }

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
            context.Table.Dispose();
        }

        public bool MoveNext()
        {
            return ++this.currentIndex < this.context.Table.Rows.Count;
        }

        public void Reset()
        {
            this.currentIndex = context.StartIndex;
        }
    }
}
