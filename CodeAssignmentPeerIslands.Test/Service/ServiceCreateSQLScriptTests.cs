using CodeAssignmentPeerIslands.Domain;
using CodeAssignmentPeerIslands.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CodeAssignmentPeerIslands.Test.Service
{
    public class ServiceCreateSQLScriptTests : ServiceCreateSqlServerScript
    {

        #region StartMock
        #region Mock Complete
        readonly string _mockQueryComplete = "SELECT \r\nTesting2.Column1 AS 'Testing2.Column1', \r\nTesting1.Column2 AS 'Testing1.Column2', \r\nTesting2.Column3 AS 'Testing2.Column3'\r\n, (\r\nSELECT \r\n.Column1 AS '.Column1', \r\n.Column2 AS '.Column2', \r\n.Column3 AS '.Column3'\r\n FROM Testing1\r\n) AS ColumnSubQuery  FROM Testing1\r\nINNER JOIN Testing2 ON Testing1.Column2 = Testing2.Column2\r\nWHERE Testing1.Column2 > 'Test1'\r\n";
        readonly string _mockWhereComplete = "WHERE Testing1.Column2 > 'Test1'";
        readonly string _mockJoinComplete = "INNER JOIN Testing2 ON Testing1.Column2 = Testing2.Column2";
        readonly Query _tableComplete = new Query()
        {
            Columns = new Column[] { new Column { TableOrigin ="Testing2", FieldName = "Column1", FieldValue = "Test", Operator = "Equal" },
                new Column { TableOrigin ="Testing1", FieldName = "Column2", FieldValue = "Test1", Operator = "GreaterThan" },
                new Column { TableOrigin ="Testing2", FieldName = "Column3", FieldValue = "Test2", Operator = "" }
            },
            Joins = new Join[] {
                new Join {
                    Type = "INNER",
                    LogicalOperator = new string[] { },
                    LeftTableColumns = new Column[] {
                        new Column { TableOrigin="Testing2", FieldName = "Column2", FieldValue = "Test1", Operator = "" }
                    },
                    RightTableColumns = new Column[]
                    {
                        new Column { TableOrigin="Testing1", FieldName = "Column2", FieldValue = "Test1", Operator = "Equal" }
                    },
                    LeftTableName = "Testing2",
                }
            },
            Name = "Testing1",
            SubQueries = new SubQuery[] {
                new SubQuery()
                {
                    FieldName = "ColumnSubQuery",
                    Tables = new Query[]
                    {
                        new Query() {
                            Columns = new Column[] { new Column { FieldName = "Column1", FieldValue = "Test", Operator="Equal" },
                                new Column { FieldName = "Column2", FieldValue = "Test1", Operator="GreaterThan" },
                                new Column { FieldName = "Column3", FieldValue = "Test2", Operator="" }
                            },
                            Joins = null,
                            Name = "Testing1",
                            SubQueries = null,
                            Where = null,
                        },
                    }
                },
            },
            Where = new Where[] { new Where {
                Columns = new Column[]
                {
                    new Column { TableOrigin="Testing1", FieldName = "Column2", FieldValue = "Test1", Operator="GreaterThan" },
                },
                LogicalOperator = "",
            } }
        };
        #endregion
        #region Mock For No Where Statement
        readonly string _mockQueryNoWhere = "SELECT \r\nTesting1.Column1 AS 'Testing1.Column1', \r\nTesting1.Column2 AS 'Testing1.Column2', \r\nTesting2.Column3 AS 'Testing2.Column3'\r\n FROM Testing1\r\nINNER JOIN Testing2 ON Testing1.Column1 = Testing2.Column2\r\n";
        readonly string _mockJoinNoWhere = "INNER JOIN Testing2 ON Testing1.Column1 = Testing2.Column2";
        readonly Query _tableNoWhere = new Query()
        {
            Columns = new Column[] { new Column {TableOrigin= "Testing1", FieldName = "Column1", FieldValue = "Test", Operator = "Equal" },
                new Column {TableOrigin= "Testing1", FieldName = "Column2", FieldValue = "Test1", Operator = "GreaterThan" },
                new Column {TableOrigin= "Testing2", FieldName = "Column3", FieldValue = "Test2", Operator = "" }
            },
            Joins = new Join[] {
                new Join {
                    Type = "INNER",
                    LogicalOperator = new string[] { },
                    LeftTableColumns = new Column[] {
                        new Column {TableOrigin= "Testing2", FieldName = "Column2", FieldValue = "Test1", Operator = "" }
                    },
                    RightTableColumns = new Column[]
                    {
                        new Column {TableOrigin= "Testing1", FieldName = "Column1", FieldValue = "Test1", Operator = "Equal" }
                    },
                    LeftTableName = "Testing2",
                }
            },
            Name = "Testing1",
        };
        #endregion Mock For No Where Statement
        #region Mock Where With Table Field
        readonly string _mockQueryWhereWithTableField = "SELECT \r\nTesting2.Column1 AS 'Testing2.Column1', \r\nTesting1.Column2 AS 'Testing1.Column2', \r\nTesting2.Column3 AS 'Testing2.Column3'\r\n, (\r\nSELECT \r\nTesting1.Column3 AS 'Testing1.Column3'\r\n FROM Testing1\r\nWHERE Testing1.Column2 > Testing2.Column2\r\n) AS ColumnSubQuery  FROM Testing1\r\nINNER JOIN Testing2 ON Testing1.Column2 = Testing2.Column2\r\nWHERE Testing1.Column2 > 'Test1'\r\n";
        readonly string _mockWhereWhereWithTableField = "WHERE Testing1.Column2 > 'Test1'";
        readonly string _mockJoinWhereWithTableField = "INNER JOIN Testing2 ON Testing1.Column2 = Testing2.Column2";
        readonly Query _tableWhereWithTableField = new Query()
        {
            Columns = new Column[] { new Column { TableOrigin ="Testing2", FieldName = "Column1", FieldValue = "Test", Operator = "Equal" },
                new Column { TableOrigin ="Testing1", FieldName = "Column2", FieldValue = "Test1", Operator = "GreaterThan" },
                new Column { TableOrigin ="Testing2", FieldName = "Column3", FieldValue = "Test2", Operator = "" }
            },
            Joins = new Join[] {
                new Join {
                    Type = "INNER",
                    LogicalOperator = new string[] { },
                    LeftTableColumns = new Column[] {
                        new Column { TableOrigin="Testing2", FieldName = "Column2", FieldValue = "Test1", Operator = "" }
                    },
                    RightTableColumns = new Column[]
                    {
                        new Column { TableOrigin="Testing1", FieldName = "Column2", FieldValue = "Test1", Operator = "Equal" }
                    },
                    LeftTableName = "Testing2",
                }
            },
            Name = "Testing1",
            SubQueries = new SubQuery[] {
                new SubQuery()
                {
                    FieldName = "ColumnSubQuery",
                    Tables = new Query[]
                    {
                        new Query() {
                            Columns = new Column[] {
                                new Column { TableOrigin="Testing1", FieldName = "Column3", FieldValue = "Test1", Operator="" }
                            },
                            Joins = null,
                            Name = "Testing1",
                            SubQueries = null,
                            Where = new Where[] { new Where {
                                Columns = new Column[]
                                {
                                    new Column { TableOrigin="Testing1", FieldName = "Column2", FieldValue = "Testing2.Column2", Operator="GreaterThan", IsTableFieldValue = true },
                                },
                                LogicalOperator = "",
                            } }
                        },
                    }
                },
            },
            Where = new Where[] { new Where {
                Columns = new Column[]
                {
                    new Column { TableOrigin="Testing1", FieldName = "Column2", FieldValue = "Test1", Operator="GreaterThan" },
                },
                LogicalOperator = "",
            } }
        };
        #endregion
        #endregion StartMock

        [Fact]
        public void CreateQueryAsyncTest_WhereWithTableField()
        {
            var result = CreateQueryAsync(_tableWhereWithTableField).Result;
            Assert.Equal(result, _mockQueryWhereWithTableField);
        }
        [Fact]
        public void CreateJoinAsyncTest_WhereWithTableField()
        {
            var result = CreateJoinAsync(_tableWhereWithTableField.Joins).Result;
            Assert.Equal(result, _mockJoinWhereWithTableField);
        }
        [Fact]
        public void CreateWhereAsyncTest_WhereWithTableField()
        {
            var result = CreateWhereAsync(_tableWhereWithTableField.Where).Result;
            Assert.Equal(result, _mockWhereWhereWithTableField);
        }

        [Fact]
        public void CreateQueryAsyncTest_NoWhere()
        {
            var result = CreateQueryAsync(_tableNoWhere).Result;
            Assert.Equal(result, _mockQueryNoWhere);
        }

        [Fact]
        public void CreateJoinAsyncTest_NoWhere()
        {
            var result = CreateJoinAsync(_tableNoWhere.Joins).Result;
            Assert.Equal(result, _mockJoinNoWhere);
        }
        [Fact]
        public void CreateQueryAsyncTest_Complete()
        {
            var result = CreateQueryAsync(_tableComplete).Result;
            Assert.Equal(result, _mockQueryComplete);
        }
        [Fact]
        public void CreateJoinAsyncTest_Complete()
        {
            var result = CreateJoinAsync(_tableComplete.Joins).Result;
            Assert.Equal(result, _mockJoinComplete);
        }
        [Fact]
        public void CreateWhereAsyncTest_Complete()
        {
            var result = CreateWhereAsync(_tableComplete.Where).Result;
            Assert.Equal(result, _mockWhereComplete);
        }

        [Fact]
        public void CreateJoinAsyncExceptionTest()
        {
            Assert.Throws<AggregateException>(() => CreateJoinAsync(null).Result);
        }
        [Fact]
        public void CreateWhereAsyncExceptionTest()
        {
            Assert.Throws<AggregateException>(() => CreateWhereAsync(null).Result);
        }
        [Fact]
        public void CreateQueryAsyncExceptionTest()
        {
            Assert.Throws<AggregateException>(() => CreateQueryAsync(null).Result);
        }
    }
}
