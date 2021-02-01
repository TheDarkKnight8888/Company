using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Company.DataAccess.Iterator;
using Company.DataAccess.Tools;

namespace Company.DataAccess
{
    public class DbSet<TEntity> : IQueryable<TEntity>, IDisposable   where TEntity : class
    {
        private readonly DbSetOptions options;
        private bool isDisposed = false;
        private readonly SqlDataAdapter adapter;
        private readonly DataSet dataSet;
        private readonly DataTable dataTable;
        private SqlParameter outIdParameter;
        private bool isOutSet = false;

        internal TEntity CurrentEntity { get; private set; }

        private readonly RowMapper mapper;
        private readonly CommandBuilder commandBuilder;

        internal DbSet(DbSetOptions options, DataSet dataSet)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.adapter = new SqlDataAdapter();
            this.dataSet = dataSet;
            string tableName = this.options.Table;
            this.adapter.SelectCommand = new SqlCommand($"SELECT * FROM {tableName} WHERE 1=2", this.options.SqlConnection);
            this.dataTable = new DataTable();
            this.adapter.Fill(dataTable);
            this.dataTable.TableName = tableName;
            this.dataSet.Tables.Add(this.dataTable);
            var sqlCommandBuilder = new SqlCommandBuilder(this.adapter);
            this.outIdParameter = new SqlParameter("@id", SqlDbType.Int);
            this.outIdParameter.Direction = ParameterDirection.Output;

            Provider = new SqlProvider<TEntity>(this);
            Expression = Expression.Constant(this);

            this.mapper = new RowMapper(this.options);
            this.commandBuilder = new CommandBuilder(options);
        }

        public Type ElementType => this.options.ElementType;

        public Expression Expression { get; internal set; }

        public IQueryProvider Provider { get; private set; }

        public TEntity Add(TEntity entity)
        {
            this.CurrentEntity = entity;
            DataRow newRow = this.dataTable.NewRow();
            this.mapper.MapObjectToRow(entity, newRow);
            string outParameterName = "@id";
            this.isOutSet = true;
            this.outIdParameter = new SqlParameter(outParameterName, SqlDbType.Int);
            adapter.InsertCommand = this.commandBuilder
                            .Insert(this.options.Table, this.dataTable.Columns)
                            .WithValues(newRow).SetOutIdentity(outParameterName, outIdParameter).Build();
            this.dataTable.Rows.Add(newRow);
            return entity;
        }

        public TEntity Remove(TEntity entity)
        {
            this.FindRowAnd(entity, (obj, row) => { row.Delete(); });
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            this.FindRowAnd(entity, (obj, row) => { this.mapper.MapObjectToRow(obj, row); });
            return entity;
        }

        public int SaveChanges()
        {
            int count = this.adapter.Update(this.dataTable);
            if (this.isOutSet)
            {
                object value = this.outIdParameter.Value;
                string pk = this.options.PrimaryKey;
                PropertyDescriptor prop = this.options.Propertis.FirstOrDefault(p => p.Name.Equals(pk));
                prop.SetValue(this.CurrentEntity, value);
                this.isOutSet = false;
                this.CurrentEntity = null;
            }

            return count;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return Provider.Execute<IEnumerable<TEntity>>(Expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal object ExecuteQuery(string members, string predicate, bool isEnumerable, params SqlParameter[] parameters)
        {
            int count = this.dataTable.Rows.Count;
            var lastIndex = count - 1;
            var command = this.commandBuilder.SelectCommand(members).FromTable(this.options.Table)
                                .When(predicate, parameters).Build();
            this.adapter.SelectCommand = command;
            this.adapter.Fill(this.dataTable);
            if (isEnumerable)
            {
                var context = new EnumeratorContext(lastIndex, this.dataTable, ElementType, this.options.Propertis);
                return new EntityEnumerable<TEntity>(context);
            }
            else
            {
                if (this.dataTable.Rows.Count > count)
                {
                    var row = this.dataTable.Rows[lastIndex + 1];
                    return this.mapper.MapRow(row);
                }
                return null;
            }
        }

        private void FindRowAnd(TEntity entity, Action<TEntity, DataRow> action)
        {
            string id = this.options.PrimaryKey;
            PropertyDescriptor descriptor = this.options.Propertis.FirstOrDefault(d => d.Name.Equals(id));
            if (descriptor != null)
            {
                object pk = descriptor.GetValue(entity);
                int index = this.dataTable.Columns.IndexOf(id);
                var enumerator = this.dataTable.Rows.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var row = (DataRow)enumerator.Current;
                    if (row.ItemArray[index].Equals(pk))
                    {
                        action(entity, row);
                        break;
                    }
                }
            }
        }

        private void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    this.dataTable.Dispose();
                    this.options.SqlConnection.Dispose();
                }
            }

            this.isDisposed = true;
        }
    }
}
