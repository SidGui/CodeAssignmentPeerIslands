using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeAssignmentPeerIslands.Domain
{
    public abstract class AQueryCreaterAsync : IQueryCreaterAsync
    {
        public abstract Task<string> CreateQueryAsync(Query table);
        protected abstract Task<string> CreateJoinAsync(Join[] joins);
        protected abstract Task<string> CreateWhereAsync(Where[] wheres);
        protected abstract string GetOperator(string whereOperator);
        protected abstract string GetSpecialOperator(string whereOperator, string values, string startValue = "", string endValue = "");

    }
}
