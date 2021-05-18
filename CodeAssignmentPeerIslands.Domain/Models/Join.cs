using System;
using System.Collections.Generic;
using System.Text;

namespace CodeAssignmentPeerIslands.Domain
{
    public class Join
    {
        public string Type { get; set; }
        public string LeftTableName { get; set; }
        public string[] LogicalOperator { get; set; }
        public Column[] RightTableColumns { get; set; }
        public Column[] LeftTableColumns { get; set; }
    }
}
