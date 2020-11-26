using API.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repository
{
  public interface ICotacaoRepository
  {
    Task<IEnumerable<Cotacao>> GetAll();

    Task<Cotacao> Get(int codigo, DateTime data);

    Task<IEnumerable<Cotacao>> Get(IEnumerable<int> codigos);
  }
}
