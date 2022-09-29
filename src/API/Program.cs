using API.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
  /// <inheritdoc/>
  public class Program
  {
    /// <inheritdoc/>
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    /// <inheritdoc/>
    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
      .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
      .ConfigureServices(services => services.AddHostedService<TimedHostedService>());
  }
}
