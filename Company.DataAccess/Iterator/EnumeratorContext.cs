using System;
using System.ComponentModel;
using System.Data;

namespace Company.DataAccess.Iterator
{
    public class EnumeratorContext
    {
        public EnumeratorContext(int index, DataTable table, Type element, PropertyDescriptor[] descriptors)
        {
            StartIndex = index;
            Table = table;
            ElementType = element;
            ElementProperties = descriptors;
        }

        public int StartIndex { get; set; }

        public DataTable Table { get; set; }

        public Type ElementType { get; set; }

        public PropertyDescriptor[] ElementProperties { get; set; }

        public object MapRowToElement(DataRow row)
        {
            object result = Activator.CreateInstance(ElementType);
            foreach (var prop in ElementProperties)
            {
                string name = prop.Name;
                object value = row[name];
                prop.SetValue(result, value == DBNull.Value ? null : value);
            }

            return result;
        }
    }
}
