using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Company.DataAccess.Tools
{
    internal class CommandBuilder
    {
        private SqlCommand command;
        private readonly StringBuilder sqlText = new StringBuilder();

        private readonly DbSetOptions options;
        private DataColumnCollection columns;

        public CommandBuilder(DbSetOptions options)
        {
            this.options = options;
        }

        public CommandBuilder SelectCommand(string members)
        {
            this.CreateNewCommand();
            sqlText.Append("SELECT").Append($" {members}"); ;
            return this;
        }

        public CommandBuilder FromTable(string tableName)
        {
            this.sqlText.Append($" FROM {tableName}");
            return this;
        }

        public CommandBuilder When(string predicate, params SqlParameter[] parameters)
        {
            if (!string.IsNullOrWhiteSpace(predicate))
            {
                this.sqlText.Append($" WHERE {predicate}");
                this.AddParameters(parameters);
            }
            
            return this;
        }

        public CommandBuilder Insert(string tableName, DataColumnCollection columns)
        {
            this.columns = columns;
            this.CreateNewCommand();
            this.sqlText.Append("INSERT ").Append(tableName);
            return this;
        }

        public CommandBuilder InOrder(string membersOrder, params SqlParameter[] parameters)
        {
            this.sqlText.Append($" ({membersOrder})");
            this.AddParameters(parameters);
            return this;
        }

        public CommandBuilder WithValues(string values, params SqlParameter[] parameters)
        {
            this.sqlText.Append($" VALUES ").Append(values);
            this.AddParameters(parameters);

            return this;
        }

        public CommandBuilder WithValues(DataRow row)
        {
            this.CreateInsertCommand(row);
            return this;
        }

        public CommandBuilder SetOutIdentity(string parameterName, SqlParameter parameter)
        {
            sqlText.Append($"SET {parameterName} = SCOPE_IDENTITY();");
            parameter.Direction = ParameterDirection.Output;
            this.command.Parameters.Add(parameter);
            return this;
        }

        public SqlCommand Build()
        {
            this.command.CommandText = this.sqlText.ToString();
            var cmd = this.command;
            this.command = null;
            this.sqlText.Clear();
            return cmd;
        }

        private void CreateInsertCommand(DataRow row)
        {
            sqlText.Append("(");
            StringBuilder valuesBuilder = new StringBuilder(" VALUES (");
            string pk = this.options.PrimaryKey;
            int i = 0;
            foreach (DataColumn column in this.columns)
            {
                string colName = column.ColumnName;
                if (!pk.Equals(colName))
                {
                    string paramName = $"@p{i++}";
                    SqlParameter parameter = new SqlParameter(paramName, row[colName]);
                    this.command.Parameters.Add(parameter);
                    this.sqlText.Append($"[{colName}], ");
                    valuesBuilder.Append($"{parameter}, ");
                }
            }
            valuesBuilder.Remove(valuesBuilder.Length - 2, 2).Append(");");
            sqlText.Remove(sqlText.Length - 2, 2).Append(")").Append(valuesBuilder);
        }


        private void AddParameters(SqlParameter[] parameters)
        {
            if (parameters != null && parameters.Length > 0)
            {
                this.command.Parameters.AddRange(parameters);
            }
        }

        private void CreateNewCommand()
        {
            var cmd = new SqlCommand();
            cmd.Connection = this.options.SqlConnection;
            this.command = cmd;
        }
    }


    interface ISelectCommand { }
}
