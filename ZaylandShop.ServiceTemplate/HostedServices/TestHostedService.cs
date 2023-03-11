using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ZaylandShop.ServiceTemplate.HostedServices;

public class TestHostedService : IHostedService, IDisposable
{
    private readonly ILogger<TestHostedService> _logger;
    private Timer? _timer;

    public TestHostedService(ILogger<TestHostedService> logger)
    {
        _logger = logger;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"{nameof(TestHostedService)} running.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromMilliseconds(10));

        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        try
        {
            _logger.LogInformation($"Start {nameof(TestHostedService)}.DoWork method.");
            
            // todo
            
            _logger.LogInformation($"End {nameof(TestHostedService)}.DoWork method.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error to {nameof(TestHostedService)}: {ex.Message}");
        }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"{nameof(TestHostedService)} is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}