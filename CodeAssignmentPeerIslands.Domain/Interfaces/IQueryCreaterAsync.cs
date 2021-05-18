using System.Threading.Tasks;

namespace CodeAssignmentPeerIslands.Domain
{
    public interface IQueryCreaterAsync
    {
        Task<string> CreateQueryAsync(Query table);
  
    }
}
