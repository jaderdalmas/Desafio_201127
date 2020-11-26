using Microsoft.Extensions.DependencyInjection;

namespace API.Service
{
  public static class IoC
  {
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
      services.AddSingleton<ICSVService, CSVService>();
      services.AddSingleton<IItemService, ItemService>();

      return services;
    }
  }
}
