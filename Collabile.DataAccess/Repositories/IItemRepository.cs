using Collabile.DataAccess.Models;
using Collabile.Shared.Helper;
using Collabile.Shared.Models.Items;

namespace Collabile.DataAccess.Repositories
{
    public interface IItemRepository : IRepository<Item, ItemSummary>
    {
    }
}
