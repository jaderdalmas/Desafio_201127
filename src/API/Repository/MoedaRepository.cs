using API.Model;
using API.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
  public class MoedaRepository : IMoedaRepository
  {
    readonly IList<Moeda> db;

    public MoedaRepository(ICSVService csv)
    {
      db = csv.GetMoeda().Result;
    }

    public Task<IEnumerable<Moeda>> Get(DateTime inicio, DateTime fim) => Task.FromResult(db.Where(x => fim > x.Data && x.Data > inicio));

    public Task<IEnumerable<Moeda>> GetAll() => Task.FromResult((IEnumerable<Moeda>)db);
  }
}
