using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;

namespace Company.DataAccess
{
    internal class DbSetOptions
    {
        private PropertyDescriptor[] descriptors;

        internal DbSetOptions(SqlConnection connection, Type type, string primapryKey)
        {
            SqlConnection = connection;
            PrimaryKey = primapryKey;
            ElementType = type;
        }

        public SqlConnection SqlConnection { get; private set; }

        public string PrimaryKey { get; private set; }

        public Type ElementType { get; private set; }

        public string Table => ElementType.Name+"s";

        public PropertyDescriptor[] Propertis 
        {
            get 
            { 
                if (this.descriptors is null)
                {
                    this.descriptors = TypeDescriptor.GetProperties(ElementType).Cast<PropertyDescriptor>().ToArray();
                }

                return this.descriptors;
            }
        }
    }
}
