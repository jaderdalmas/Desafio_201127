using API.Model;
using API.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Service
{
  public class ItemService : IItemService
  {
    private readonly ICSVService _cSVService;

    private readonly ICotacaoRepository _cotacaoRepository;
    private readonly IDeParaRepository _deParaRepository;
    private readonly IMoedaRepository _moedaRepository;

    public ItemService(ICSVService cSVService, ICotacaoRepository cotacaoRepository, IDeParaRepository deParaRepository, IMoedaRepository moedaRepository)
    {
      _cSVService = cSVService;

      _cotacaoRepository = cotacaoRepository;
      _deParaRepository = deParaRepository;
      _moedaRepository = moedaRepository;
    }

    public async Task PostCSV(Item item)
    {
      var moeda = await _moedaRepository.Get(item.DataInicio, item.DataFim).ConfigureAwait(false);

      var result = new List<MoedaCotacao>();
      foreach (var m in moeda.AsParallel())
      {
        var codigo = (await _deParaRepository.Get(m.Id).ConfigureAwait(false))?.CodCotacao ?? 0;

        var valor = (await _cotacaoRepository.Get(codigo, m.Data).ConfigureAwait(false))?.Valor ?? 0;

        result.Add(new MoedaCotacao(m, codigo, valor));
      }

      await _cSVService.PostMoedaCotacao(result).ConfigureAwait(false);
    }
  }
}
