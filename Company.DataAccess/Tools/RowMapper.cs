using System;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace Company.DataAccess.Tools
{
    internal class RowMapper
    {
        private readonly DbSetOptions context;

        internal RowMapper(DbSetOptions context)
        {
            this.context = context;
        }

        internal object MapRow(DataRow row)
        {
            object entity = Activator.CreateInstance(this.context.ElementType);
            Map(row, entity, (prop, dr, obj) =>
            {
                string name = prop.Name;
                object value = dr[name];
                prop.SetValue(obj, (value == DBNull.Value) ? null : value);
            });

            return entity;
        }

        internal void MapObjectToRow(object entity, DataRow row)
        {
            Map(row, entity, (prop, dr, obj) =>
            {
                string name = prop.Name;
                object value = prop.GetValue(entity);
                dr[name] = (value is null) ? DBNull.Value : value;
            });
        }

        private void Map(DataRow row, object entity, Action<PropertyDescriptor, DataRow, object> action)
        {
            
            Type dateTimeType = typeof(DateTime);
            Type stringType = typeof(string);
            var properties = this.context.Propertis.Where(p => p.PropertyType.IsPrimitive || p.PropertyType.IsValueType || p.PropertyType.Equals(stringType) || p.PropertyType.Equals(dateTimeType));
            foreach (var prop in properties)
            {
                action(prop, row, entity);
            }
        }
    }
}
