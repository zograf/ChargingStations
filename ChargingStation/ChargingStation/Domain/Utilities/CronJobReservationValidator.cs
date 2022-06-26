using ChargingStation.Domain.Models;
using ChargingStation.Service;

namespace ChargingStation.Domain.Utilities
{
    public class CronJobReservationValidator : CronJobService
    {
        public IServiceProvider _provider;
        public CronJobReservationValidator(IScheduleConfig<CronJobReservationValidator> config, 
            IServiceProvider serviceProvider) : base(config.CronExpression, config.TimeZoneInfo)
        {
            _provider = serviceProvider;
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            using (IServiceScope scope = _provider.CreateScope())
            {
                IReservationService reservationService = scope.ServiceProvider.GetRequiredService<IReservationService>();
                List<ClientDomainModel> clients = await reservationService.CheckValidity();
                // TODO: Do something
                Console.WriteLine("HELLO");
            }
        }
    }
}