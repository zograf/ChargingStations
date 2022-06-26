using ChargingStation.Data.Entity;
using ChargingStation.Domain.Models;
using ChargingStation.Domain.Utilities;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface IPriceService : IService<BasePriceDomainModel>
{
    public Task<decimal> GetPrice(decimal stationId, DateTime timeOfCharging);
}

public class PriceService : IPriceService
{
    private readonly IBasePriceRepository _basePriceRepository;
    private readonly IChargingSpotRepository _chargingSpotRepository;
    private readonly IReservationRepository _reservationRepository;

    public PriceService(IBasePriceRepository basePriceRepository,
        IChargingSpotRepository chargingSpotRepository,
        IReservationRepository reservationRepository)
    {
        _basePriceRepository = basePriceRepository;
        _chargingSpotRepository = chargingSpotRepository;
        _reservationRepository = reservationRepository;
    }
    
    public async Task<List<BasePriceDomainModel>> GetAll()
    {
        List<BasePrice> basePrices = await _basePriceRepository.GetAll();
        List<BasePriceDomainModel> result = new List<BasePriceDomainModel>();
        foreach (var item in basePrices)
            result.Add(ParseToModel(item));
        return result;
    }

    public static BasePriceDomainModel ParseToModel(BasePrice basePrice)
    {
        return new BasePriceDomainModel
        {
            Id = basePrice.Id,
            IsDeleted = basePrice.IsDeleted,
            StationId = basePrice.StationId,
            ApplyTime = basePrice.ApplyTime,
            DayAmount = basePrice.DayAmount,
            NightAmount = basePrice.NightAmount
        };
    }

    public async Task<decimal> GetPrice(decimal stationId, DateTime timeOfCharging)
    {
        BasePrice basePrice = await _basePriceRepository.GetByStation(stationId);
        decimal price;
        price = DateTimeUtils.IsDay(timeOfCharging) ? basePrice.DayAmount : basePrice.NightAmount;
        price *= await CalculateBusyness(stationId, timeOfCharging);
        
        return price;
    }

    private async Task<decimal> CalculateBusyness(decimal stationId, DateTime timeOfCharging)
    {
        IEnumerable<ChargingSpot> spots = await _chargingSpotRepository.GetByStation(stationId);
        decimal count = 0;
        decimal maxCapacity = spots.Count();
        foreach (ChargingSpot spot in spots)
        {
            IEnumerable<Reservation> reservations = await _reservationRepository.GetByChargingSpot(spot.Id);
            foreach (Reservation reservation in reservations)
            {
                if(timeOfCharging >= reservation.StartTime && timeOfCharging <= reservation.EndTime)
                    count++;
            }
        }
        return count * 1.25m / maxCapacity;
    }
}
