using API.Repository;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace API.Service
{
  public class TimedHostedService : IHostedService, IDisposable
  {
    private int executionCount = 0;
    private readonly ILogger<TimedHostedService> _logger;
    private Timer _timer;

    private IItemService _itemService;
    private IItemRepository _itemRepository;

    public TimedHostedService(ILogger<TimedHostedService> logger, IItemService itemService, IItemRepository itemRepository)
    {
      _logger = logger;

      _itemService = itemService;
      _itemRepository = itemRepository;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("Timed Hosted Service running.");

      _timer = new Timer(DoWork, null, TimeSpan.Zero,
          TimeSpan.FromMinutes(2));

      return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
      var count = Interlocked.Increment(ref executionCount);

      var watch = new Stopwatch();
      watch.Start();
      var result = _itemRepository.LIFO().Result;
      if (result != null)
        _itemService.PostCSV(result).Wait();
      watch.Stop();

      _logger.LogInformation("Timed Hosted Service is working. Count: {Count} | Time: {Elapsed}", count, watch.Elapsed);
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("Timed Hosted Service is stopping.");

      _timer?.Change(Timeout.Infinite, 0);

      return Task.CompletedTask;
    }

    public void Dispose()
    {
      _timer?.Dispose();
    }
  }
}
