using CodeAssignmentPeerIslands.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeAssignmentPeerIslands.Service
{
    public class ServiceCreateSqlServerScript : AQueryCreaterAsync
    {

        private static Dictionary<string, string> _Operators = new Dictionary<string, string>();
        private static Dictionary<string, string> _SpecialOperators = new Dictionary<string, string>();

        public ServiceCreateSqlServerScript()
        {
            if (_Operators.Count == 0)
            {
                _Operators.Add("equal", "=");
                _Operators.Add("smallerequal", "<=");
                _Operators.Add("smallerthan", "<");
                _Operators.Add("greaterequal", ">=");
                _Operators.Add("greaterthan", ">");
                _Operators.Add("diferent", "<>");
            }
            if (_SpecialOperators.Count == 0)
            {
                _SpecialOperators.Add("in", "in ('{0}')");
                _SpecialOperators.Add("between", "BETWEEN '{0}' AND {1}");
                _SpecialOperators.Add("like", "LIKE ('{0}{1}{2}')");
                _SpecialOperators.Add("notlike", "NOT LIKE('{0}{1}{2}')");
            }
        }

        /// <summary>
        /// Create a dynamic SELECT based in JSON parsed file
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public override async Task<string> CreateQueryAsync(Query table)
        {
            StringBuilder queryBuilder = new StringBuilder();
            try
            {

                queryBuilder.AppendLine($"SELECT {(table.Columns.Length == 0 ? $"{table.Name}.*" : "")}");
                int index = 0;
                foreach (var column in table.Columns)
                {
                    string alias = string.IsNullOrEmpty(column.Alias) ? $"'{column.TableOrigin}.{column.FieldName}'" : $"'{column.Alias}'";
                    queryBuilder.AppendLine($"{column.TableOrigin}.{column.FieldName} AS {alias}{(index < table.Columns.Length - 1 ? ", " : "")}");
                    index++;
                }
                if (table.SubQueries != null)
                {

                    foreach (var subQuery in table.SubQueries)
                    {
                        queryBuilder.AppendLine(", (");
                        foreach (var subTable in subQuery.Tables)
                        {
                            queryBuilder.Append(await this.CreateQueryAsync(subTable));
                        }
                        queryBuilder.Append($") AS {subQuery.FieldName} ");
                    }

                }

                queryBuilder.AppendLine($" FROM {table.Name}");
                if (table.Joins != null)
                {
                    queryBuilder.AppendLine(await this.CreateJoinAsync(table.Joins));
                }
                if (table.Where != null)
                {
                    queryBuilder.AppendLine(await this.CreateWhereAsync(table.Where));
                }
            }
            catch (Exception error)
            {
                throw new Exception($"[Error CreateQueryAsync] => Something went wrong, check the log. \n Original error: {error.Message}");
            }
            return queryBuilder.ToString();
        }

        /// <summary>
        /// Create JOIN statement based in JSON parsed file
        /// </summary>
        /// <param name="joins"></param>
        /// <returns></returns>
        protected override async Task<string> CreateJoinAsync(Join[] joins)
        {
            StringBuilder joinBuilder = new StringBuilder();
            try
            {


                foreach (var join in joins)
                {
                    switch (join.Type.ToUpper())
                    {
                        case "INNER": joinBuilder.Append("INNER JOIN"); break;
                        case "LEFT": joinBuilder.Append("LEFT JOIN"); break;
                    }
                    joinBuilder.Append($" {join.LeftTableName} ON");
                    int indexLeftColumn = 0;
                    foreach (var column in join.RightTableColumns)
                    {
                        string sqlOperator = this.GetOperator(column.Operator.ToLower());
                        bool useSpecialOperator = false;
                        if (string.IsNullOrEmpty(sqlOperator))
                        {
                            sqlOperator = this.GetSpecialOperator(column.Operator.ToLower(), column.FieldValue, column.StartValue, column.EndValue);
                            useSpecialOperator = true;
                        }
                        string rightField = $"{column.TableOrigin}.{column.FieldName}";
                        string leftField = $"{join.LeftTableColumns[indexLeftColumn].TableOrigin}.{join.LeftTableColumns[indexLeftColumn].FieldName}";

                        joinBuilder.Append($" {rightField} {sqlOperator} {(useSpecialOperator ? "" : leftField)}");
                        if(indexLeftColumn < join.LogicalOperator.Length)
                        {
                            joinBuilder.Append($" {join.LogicalOperator[indexLeftColumn]}");
                        }
                        indexLeftColumn++;
                    }
                    indexLeftColumn = 0;
                }
            }
            catch (Exception error)
            {
                throw new Exception($"[Error CreateJoinAsync] => Something went wrong, check the log. \n Original error: {error.Message}");
            }
            return joinBuilder.ToString();
        }

        /// <summary>
        /// Create WHERE statement based in JSON parsed file
        /// </summary>
        /// <param name="wheres"></param>
        /// <returns></returns>
        protected override async Task<string> CreateWhereAsync(Where[] wheres)
        {
            StringBuilder whereBuilder = new StringBuilder();
            try
            {
                whereBuilder.Append("WHERE ");
                foreach (var where in wheres)
                {
                    foreach (var column in where.Columns)
                    {
                        string sqlOperator = this.GetOperator(column.Operator.ToLower());
                        bool useSpecialOperator = false;
                        if (string.IsNullOrEmpty(sqlOperator))
                        {
                            sqlOperator = this.GetSpecialOperator(column.Operator.ToLower(), column.FieldValue, column.StartValue, column.EndValue);
                            useSpecialOperator = true;
                        }
                        string value = "";
                        if(!column.IsTableFieldValue)
                        {
                            value = $"{ (useSpecialOperator ? "" : $"'{column.FieldValue}'") }";
                        } else
                        {
                            value = $"{column.FieldValue}";
                        }
                        string fieldName = $"{column.TableOrigin}.{column.FieldName}";
                        whereBuilder.Append($"{fieldName} {sqlOperator} {value}");
                    }
                    if (!string.IsNullOrEmpty(where.LogicalOperator))
                    {
                        whereBuilder.Append($"{where.LogicalOperator} ");
                    }
                }
            }
            catch (Exception error)
            {
                throw new Exception($"[Error CreateWhereAsync] => Something went wrong, check the log. \n Original error: {error.Message}");
            }
            return whereBuilder.ToString();
        }

        /// <summary>
        /// Get the Operator to replace in WHERE statement
        /// </summary>
        /// <param name="whereOperator"></param>
        /// <returns></returns>
        protected override string GetOperator(string whereOperator)
        {
            string operation;
            if (!_Operators.TryGetValue(whereOperator, out operation))
            {
                return null;
            }
            return operation;
        }

        /// <summary>
        /// Get the Special Operator to replace in WHERE statement
        /// </summary>
        /// <param name="whereOperator"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        protected override string GetSpecialOperator(string whereOperator, string values, string startValue = "", string endValue = "")
        {
            string operation;
            if (!_SpecialOperators.TryGetValue(whereOperator, out operation))
            {
                operation = whereOperator;
            }
            switch (whereOperator)
            {
                case "in": operation = string.Format(operation, string.Join(",", $"'{values}'")); break;
                case "between": operation = string.Format(operation, startValue, $"{(string.IsNullOrEmpty(endValue) ? "GETDATE()" : $"'{endValue}'")}"); break;
                case "like":
                case "notlike": operation = string.Format(operation, startValue, values, endValue); break;
            }

            return operation;
        }
    }
}
