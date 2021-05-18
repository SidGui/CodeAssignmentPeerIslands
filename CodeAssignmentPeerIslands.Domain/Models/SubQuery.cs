using System;
using System.Collections.Generic;
using System.Text;

namespace CodeAssignmentPeerIslands.Domain
{
    public class SubQuery
    {
        public string FieldName { get; set; }
        public Query[] Tables { get; set; }
    }
}
