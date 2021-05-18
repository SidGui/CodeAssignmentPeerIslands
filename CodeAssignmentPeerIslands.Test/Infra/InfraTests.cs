using System;
using Xunit;
using Moq;
using System.IO.Abstractions;
using System.Threading.Tasks;
using CodeAssignmentPeerIslands.Infra;
using CodeAssignmentPeerIslands.Domain;

namespace CodeAssignmentPeerIslands.Test
{

    public class InfraTests
    {  
        readonly Mock<IFileSystem> _fileSystem;
        public InfraTests()
        {
            _fileSystem = new Mock<IFileSystem>();
            _fileSystem.Setup(f => f.File.ReadAllText(It.IsAny<string>())).Returns("{\r\n  \"tables\": [\r\n    {\r\n      \"name\": \"table1\",\r\n      \"alias\":  \"T1\",\r\n      \"columns\": [\r\n        {\r\n          \"operator\": \"IN\",\r\n          \"fieldName\": \"column1\",\r\n          \"fieldValue\": \"value1\"\r\n        },\r\n        {\r\n          \"operator\": \"Equal\",\r\n          \"fieldName\": \"column2\",\r\n          \"fieldValue\": \"value2\"\r\n        },\r\n        {\r\n          \"operator\": \"GreaterEqual\",\r\n          \"fieldName\": \"column3\",\r\n          \"fieldValue\": \"value3\"\r\n        },\r\n        {\r\n          \"operator\": \"SmallerEqual\",\r\n          \"fieldName\": \"column4\",\r\n          \"fieldValue\": \"value4\"\r\n        },\r\n        {\r\n          \"operator\": \"NOT NULL\",\r\n          \"fieldName\": \"column5\",\r\n          \"fieldValue\": \"value5\"\r\n        }\r\n      ],\r\n      \"where\": [\r\n        {\r\n          \"logicalOperator\": \"AND\",\r\n          \"columns\": [\r\n            {\r\n              \"operator\": \"IN\",\r\n              \"fieldName\": \"column1\",\r\n              \"fieldValue\": \"value1\",\r\n            },\r\n          ],\r\n        },\r\n        {\r\n          \"logicalOperator\": \"\",\r\n          \"columns\": [\r\n            {\r\n              \"operator\": \"Equal\",\r\n              \"fieldName\": \"column1\",\r\n              \"fieldValue\": \"value1\",\r\n            }\r\n          ]\r\n        }\r\n      ],\r\n      \"joins\": [\r\n        {\r\n          \"type\": \"INNER\",\r\n          \"rightTableName\": \"table1\",\r\n          \"leftTableName\": \"table2\",\r\n          \"rightTableColumns\": {\r\n            \"fieldName\": \"column1\",\r\n            \"fieldValue\": \"value1\",\r\n            \"operator\": \"Equal\"\r\n          },\r\n          \"leftTableColumns\": {\r\n            \"fieldName\": \"column2\",\r\n            \"fieldValue\": \"value2\"\r\n          }\r\n        }\r\n      ],\r\n      \"subQueries\": [\r\n        {\r\n          \"fieldName\": \"sub1\",\r\n          \"tables\": [\r\n            {\r\n              \"name\": \"table1\",\r\n              \"alias\": \"\",\r\n              \"columns\": [\r\n                {\r\n                  \"operator\": \"IN\",\r\n                  \"fieldName\": \"column1\",\r\n                  \"fieldValue\": \"value1\"\r\n                },\r\n                {\r\n                  \"operator\": \"Equal\",\r\n                  \"fieldName\": \"column2\",\r\n                  \"fieldValue\": \"value2\"\r\n                }\r\n              ],\r\n              \"where\": [\r\n                {\r\n                  \"logicalOperator\": \"AND\",\r\n                  \"columns\": [\r\n                    {\r\n                      \"operator\": \"IN\",\r\n                      \"fieldName\": \"column5\",\r\n                      \"fieldValue\": \"value5\"\r\n                    }\r\n                  ]\r\n                },\r\n                {\r\n                  \"logicalOperator\": \"\",\r\n                  \"columns\": [\r\n                    {\r\n                      \"operator\": \"Equal\",\r\n                      \"fieldName\": \"column5\",\r\n                      \"fieldValue\": \"value5\"\r\n                    }\r\n                  ]\r\n                }\r\n              ]\r\n            }\r\n          ]\r\n        }\r\n      ]\r\n    },\r\n    {\r\n      \"name\": \"table2\",\r\n      \"columns\": [\r\n        {\r\n          \"operator\": \"SmallerEqual\",\r\n          \"fieldName\": \"column4\",\r\n          \"fieldValue\": \"value4\"\r\n        },\r\n        {\r\n          \"operator\": \"NOT NULL\",\r\n          \"fieldName\": \"column5\",\r\n          \"fieldValue\": \"value5\"\r\n        }\r\n      ],\r\n      \"joins\": [\r\n        {\r\n          \"type\": \"LEFT\",\r\n          \"rightTableName\": \"table2\",\r\n          \"leftTableName\": \"table3\",\r\n          \"rightTableColumns\": {\r\n            \"fieldName\": \"column1\",\r\n            \"fieldValue\": \"value1\",\r\n            \"operator\": \"Equal\"\r\n          },\r\n          \"leftTableColumns\": {\r\n            \"fieldName\": \"column2\",\r\n            \"fieldValue\": \"value2\"\r\n          }\r\n        }\r\n      ]\r\n    },\r\n    {\r\n      \"name\": \"table3\",\r\n      \"columns\": [\r\n        {\r\n          \"operator\": \"SmallerEqual\",\r\n          \"fieldName\": \"column4\",\r\n          \"fieldValue\": \"value4\"\r\n        },\r\n        {\r\n          \"operator\": \"NOT NULL\",\r\n          \"fieldName\": \"column5\",\r\n          \"fieldValue\": \"value5\"\r\n        }\r\n      ],\r\n      \"where\": [\r\n        {\r\n          \"logicalOperator\": \"AND\",\r\n          \"columns\": [\r\n            {\r\n              \"operator\": \"between\",\r\n              \"fieldName\": \"column1\",\r\n              \"fieldValue\": \"value1\"\r\n            }\r\n          ]\r\n        },\r\n        {\r\n          \"logicalOperator\": \"OR\",\r\n          \"columns\": [\r\n            {\r\n              \"operator\": \"like\",\r\n              \"fieldName\": \"column1\",\r\n              \"fieldValue\": \"value1\"\r\n            }\r\n          ]\r\n        },\r\n        {\r\n          \"logicalOperator\": \"\",\r\n          \"columns\": [\r\n            {\r\n              \"operator\": \"notlike\",\r\n              \"fieldName\": \"column1\",\r\n              \"fieldValue\": \"value1\"\r\n            }\r\n          ]\r\n        }\r\n      ]\r\n    }\r\n  ]\r\n}");
        }
      
        [Fact]
        public void Infra_Read_File_Test()
        {
            var infra = new ReadJsonFile();
            var result = infra.ConvertJsonToObject("");
            Console.WriteLine(result);
            Assert.IsType<Task<SQL>>(result);
        }
    }
}