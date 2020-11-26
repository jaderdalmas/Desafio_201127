using API.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
  public interface IMoedaRepository
  {
    Task<IEnumerable<Moeda>> GetAll();

    Task<IEnumerable<Moeda>> Get(DateTime inicio, DateTime fim);
  }
}
