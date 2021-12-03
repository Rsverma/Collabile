using Collabile.DataAccess.Models;
using Collabile.Shared.Helper;

namespace Collabile.DataAccess.Repositories
{
    public interface IItemRepository : IRepository<Item, ItemSummary>
    {
    }
}
