using ChargingStation.Service;

namespace ChargingStation.Domain.Utilities
{
    public class CronJobStateManager : CronJobService
    {
        public IServiceProvider _provider;

        public CronJobStateManager(IScheduleConfig<CronJobReservationValidator> config,
            IServiceProvider serviceProvider) : base(config.CronExpression, config.TimeZoneInfo)
        {
            _provider = serviceProvider;
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            using (IServiceScope scope = _provider.CreateScope())
            {
                IChargingSpotService chargingSpotService =
                    scope.ServiceProvider.GetRequiredService<IChargingSpotService>();
                _ = await chargingSpotService.ManageStates();
                await Task.Delay(10000);
                _ = await chargingSpotService.ManageStates();
                await Task.Delay(10000);
                _ = await chargingSpotService.ManageStates();
                await Task.Delay(10000);
                _ = await chargingSpotService.ManageStates();
            }
        }
    }
}