using API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Service
{
  public interface ICSVService
  {
    Task<IList<Cotacao>> GetCotacao();

    Task<IList<Moeda>> GetMoeda();

    Task PostMoedaCotacao(IEnumerable<MoedaCotacao> moedas);
  }
}
