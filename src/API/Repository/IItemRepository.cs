using API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
  public interface IItemRepository
  {
    Task<bool> Add(Item item);

    Task<bool> Add(IEnumerable<Item> items);

    Task<Item> LIFO();
  }
}
