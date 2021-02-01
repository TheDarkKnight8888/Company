using System.Collections;
using System.Collections.Generic;

namespace Company.DataAccess.Iterator
{
    public class EntityEnumerable<TEnitity> : IEnumerable<TEnitity> where TEnitity : class
    {
        private readonly EnumeratorContext context;

        public EntityEnumerable(EnumeratorContext context)
        {
            this.context = context;
        }

        public IEnumerator<TEnitity> GetEnumerator()
        {
            return new EntityEnumerator<TEnitity>(this.context);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
