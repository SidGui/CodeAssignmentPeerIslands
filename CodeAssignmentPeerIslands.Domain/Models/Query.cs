using System;
using System.Collections.Generic;
using System.Text;

namespace CodeAssignmentPeerIslands.Domain
{
    public class Query
    {
        public Column[] Columns { get; set; }
        public Join[] Joins { get; set; }
        public string Name { get; set; }
        public SubQuery[] SubQueries { get; set; }
        public Where[] Where { get; set; }
    }
}
