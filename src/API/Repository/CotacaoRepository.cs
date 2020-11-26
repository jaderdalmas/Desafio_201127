using API.Model;
using API.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
  public class CotacaoRepository : ICotacaoRepository
  {
    readonly IList<Cotacao> db;

    public CotacaoRepository(ICSVService csv)
    {
      db = csv.GetCotacao().Result;
    }

    public Task<Cotacao> Get(int codigo, DateTime data) => Task.FromResult(db.FirstOrDefault(x => x.Codigo == codigo && x.Data == data));

    public Task<IEnumerable<Cotacao>> Get(IEnumerable<int> codigos) => Task.FromResult(db.Where(x => codigos.Contains(x.Codigo)));

    public Task<IEnumerable<Cotacao>> GetAll() => Task.FromResult((IEnumerable<Cotacao>)db);
  }
}
