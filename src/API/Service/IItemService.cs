using API.Model;
using System.Threading.Tasks;

namespace API.Service
{
  public interface IItemService
  {
    Task GetItem(Item item);
  }
}
