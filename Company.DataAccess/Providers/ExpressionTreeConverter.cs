using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;

namespace Company.DataAccess
{
    internal class ExpressionTreeConverter<TResult>
    {
        private readonly List<SqlParameter> parameters = new List<SqlParameter>();
        private readonly StringBuilder predicateBuilder = new StringBuilder();
        private bool isEnumerable;

        internal SqlParameter[] SplParameters => parameters.ToArray();

        internal string ConvertToSqlPredicate(Expression expression, bool isEnumerable)
        {
            this.isEnumerable = isEnumerable;

            try
            {
                if (expression.NodeType == ExpressionType.Lambda)
                {
                    ConvertLambda(expression, isEnumerable);
                }
                else if (expression.NodeType == ExpressionType.Call)
                {
                    ConvertCall(expression, isEnumerable);
                }
            }
            catch(Exception ex)
            {
                throw new ExpressionConvertingException(ex.Message, ex.InnerException);
            }

            return this.predicateBuilder.ToString();
        }

        private void ConvertCall(Expression expression, bool isEnumerable)
        {
            Expression call;
            if (isEnumerable)
            {
                call = ((MethodCallExpression)expression).Arguments[1];
                if(call.NodeType == ExpressionType.Quote)
                {
                    ConvertQuote(call);
                }
            }
        }

        private void ConvertQuote(Expression expression)
        {
            Expression operand = ((UnaryExpression)expression).Operand;
            if (operand.NodeType == ExpressionType.Lambda)
            {
                ConvertLambda(operand, this.isEnumerable);
            }

        }

        private void ConvertLambda(Expression expression, bool isEnumerable)
        {
            Expression lambda;
            if (isEnumerable)
            {
                lambda = (Expression)expression.GetType().GetProperty("Body").GetValue(expression);
            }
            else
            {
                lambda = ((Expression<Func<TResult, bool>>)expression).Body;
            } 

            if (lambda.NodeType == ExpressionType.Quote)
            {
                this.ConvertQuote(expression);
            }
            else if (lambda.NodeType != ExpressionType.Call)
            {
                ConvertBinary(lambda);
            }
        }

        private void ConvertBinary(Expression expression)
        {
            var binary = (BinaryExpression)expression;
            if (binary.NodeType == ExpressionType.Equal)
            {
                var left = binary.Left;
                var right = binary.Right;
                if (left.NodeType == ExpressionType.MemberAccess && right.NodeType == ExpressionType.Constant)
                {
                    var member = (MemberExpression)left;
                    var constant = (ConstantExpression)right;
                    var name = member.Member.Name;
                    var value = constant.Value;

                    
                    this.predicateBuilder.Append($"[{name}]");
                    if (binary.NodeType == ExpressionType.Equal)
                    {
                        if (value is null)
                        {
                            this.predicateBuilder.Append(" IS NULL");
                            return;
                        }
                        else
                        {
                            this.predicateBuilder.Append(" = ");
                        }
                    }
                    string parameterName = $"@p{parameters.Count}";
                    var parameter = new SqlParameter(parameterName, value ?? DBNull.Value);
                    this.parameters.Add(parameter);
                    this.predicateBuilder.Append(parameterName);
                }
                else if (left.NodeType == ExpressionType.MemberAccess && right.NodeType == ExpressionType.MemberAccess)
                {
                    var memberLeft = (MemberExpression)left;
                    var memberRight = (MemberExpression)right;
                    var name = memberLeft.Member.Name;
                    object value = Expression.Lambda(memberRight).Compile().DynamicInvoke();

                    this.predicateBuilder.Append($"[{name}]");
                    if (binary.NodeType == ExpressionType.Equal)
                    {
                        if(value is null)
                        {
                            this.predicateBuilder.Append(" IS NUll");
                            return;
                        }
                        else
                        {
                            this.predicateBuilder.Append(" = ");
                        }
                    }

                    string parameterName = $"@p{parameters.Count}";
                    this.predicateBuilder.Append(parameterName);
                    var parameter = new SqlParameter(parameterName, value ?? DBNull.Value);
                    this.parameters.Add(parameter);
                }
                else if (left.NodeType == ExpressionType.MemberAccess && right.NodeType == ExpressionType.Convert)
                {
                    var memberLeft = (MemberExpression)left;
                    var memberRight = (MemberExpression)((UnaryExpression)right).Operand;

                    var name = memberLeft.Member.Name;
                    object value = Expression.Lambda(memberRight).Compile().DynamicInvoke();

                    this.predicateBuilder.Append($"[{name}]");
                    if (binary.NodeType == ExpressionType.Equal)
                    {
                        if (value is null)
                        {
                            this.predicateBuilder.Append(" IS NUll");
                            return;
                        }
                        else
                        {
                            this.predicateBuilder.Append(" = ");
                        }
                    }

                    string parameterName = $"@p{parameters.Count}";
                    this.predicateBuilder.Append(parameterName);
                    var parameter = new SqlParameter(parameterName, value ?? DBNull.Value);
                    this.parameters.Add(parameter);
                }
            }
        }
    }
}
