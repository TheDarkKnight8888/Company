using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Company.DataAccess
{
    public class DbContext : IDisposable
    {
        private const string primaryKey = "Id";
        private bool isDisposed = false;
        private readonly SqlConnection connection;
        private readonly DataSet dataSet = new DataSet();
        
        public DbContext(string connectionString)
        {
            this.connection = new SqlConnection(connectionString);
            try
            {
                this.connection.Open();
            }
            catch (Exception ex)
            {
                throw new DbConnectionException(ex.Message, ex.InnerException);
            }
            
            this.InitializeDbSets();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    this.connection.Dispose();
                }
            }

            this.isDisposed = true;
        }

        private void InitializeDbSets()
        {
            var dbSetType = typeof(DbSet<>);
            var dbSetDescriptors = TypeDescriptor.GetProperties(this).Cast<PropertyDescriptor>()
                .Where(p => p.PropertyType.IsConstructedGenericType && p.PropertyType.GetGenericTypeDefinition().Equals(dbSetType));
            var entityTypes = dbSetDescriptors.Select(p => p.PropertyType.GenericTypeArguments[0]);
            foreach (var descriptor in dbSetDescriptors)
            {
                Type entityType = descriptor.PropertyType.GenericTypeArguments[0];
                Type entityDbSetType = dbSetType.MakeGenericType(entityType);
                var options = new DbSetOptions(this.connection, entityType, primaryKey);
                object dbSet = Activator.CreateInstance(entityDbSetType, BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { options, this.dataSet }, CultureInfo.InvariantCulture);
                descriptor.SetValue(this, dbSet);
            }
        }
    }
}
