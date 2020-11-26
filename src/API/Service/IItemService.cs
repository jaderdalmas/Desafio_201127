using API.Model;
using System.Threading.Tasks;

namespace API.Service
{
  public interface IItemService
  {
    Task PostCSV(Item item);
  }
}
