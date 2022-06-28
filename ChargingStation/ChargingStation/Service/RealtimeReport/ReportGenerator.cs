using ChargingStation.Domain.Utilities;
using Microsoft.AspNetCore.SignalR;

namespace ChargingStation.Service.RealtimeReport;

public class ReportGenerator : CronJobService
{
    public IServiceProvider _provider;
    private IMainHub _hubContext;

    public ReportGenerator(IScheduleConfig<ReportGenerator> config,
        IServiceProvider serviceProvider) : base(config.CronExpression, config.TimeZoneInfo)
    {
        _provider = serviceProvider;
        _hubContext = _provider.CreateScope().ServiceProvider.GetRequiredService<IMainHub>();
    }

    public override async Task DoWork(CancellationToken cancellationToken)
    {
        using (IServiceScope scope = _provider.CreateScope())
        {
            IChargingSpotService chargingSpotService = scope.ServiceProvider.GetRequiredService<IChargingSpotService>();

            var spots = chargingSpotService.GetAll();
            foreach (var spot in await spots)
            {
                try
                {
                    _hubContext.Send((int)spot.Id, (int)spot.State);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}