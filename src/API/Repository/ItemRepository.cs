using API.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
  public class ItemRepository : IItemRepository
  {
    readonly IList<Item> db;

    public ItemRepository()
    {
      db = new List<Item>();
    }

    public async Task<bool> Add(IEnumerable<Item> items)
    {
      var result = new List<bool>();
      foreach (var item in items)
        result.Add(await Add(item).ConfigureAwait(false));

      return result.TrueForAll(x => x);
    }

    public Task<bool> Add(Item item)
    {
      if (item == null)
        return Task.FromResult(false);

      db.Add(item);
      return Task.FromResult(true);
    }

    public Task<Item> LIFO()
    {
      if (db?.Any() != true)
        return Task.FromResult((Item)null);

      var result = db.Last();
      db.Remove(result);
      return Task.FromResult(result);
    }
  }
}
