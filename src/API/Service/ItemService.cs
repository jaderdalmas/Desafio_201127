using API.Model;
using API.Repository;
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

    public async Task GetItem(Item item)
    {
      var moeda = await _moedaRepository.Get(item.DataInicio, item.DataFim).ConfigureAwait(false);

      var result = moeda.Select(x => new MoedaCotacao(x));
      foreach (var r in result.AsParallel())
      {
        r.Codigo = (await _deParaRepository.Get(r.Id).ConfigureAwait(false))?.CodCotacao ?? 0;

        r.Valor = (await _cotacaoRepository.Get(r.Codigo, r.Data).ConfigureAwait(false))?.Valor ?? 0;
      }

      await _cSVService.PostMoedaCotacao(result).ConfigureAwait(false);
    }
  }
}
