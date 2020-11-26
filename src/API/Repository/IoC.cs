using Microsoft.Extensions.DependencyInjection;

namespace API.Repository
{
  public static class IoC
  {
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
      services.AddSingleton<IItemRepository, ItemRepository>();

      return services;
    }
  }
}
