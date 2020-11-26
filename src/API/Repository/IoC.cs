using Microsoft.Extensions.DependencyInjection;

namespace API.Repository
{
  public static class IoC
  {
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
      services.AddSingleton<ICotacaoRepository, CotacaoRepository>();
      services.AddSingleton<IDeParaRepository, DeParaRepository>();
      services.AddSingleton<IItemRepository, ItemRepository>();
      services.AddSingleton<IMoedaRepository, MoedaRepository>();

      return services;
    }
  }
}
