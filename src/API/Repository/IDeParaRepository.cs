using API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
  public interface IDeParaRepository
  {
    Task<IEnumerable<DePara>> GetAll();
  }
}
