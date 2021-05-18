using System;
using System.Collections.Generic;
using System.Text;

namespace CodeAssignmentPeerIslands.Domain
{
    public class Where
    {
        public string LogicalOperator { get; set; }
        public Column[] Columns { get; set; }
        
    }
}
