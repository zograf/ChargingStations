using ChargingStation.Data.Entity;
using ChargingStation.Domain.DTOs;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface IChargingService : IService<ChargingDomainModel>
{
    public Task<ChargingDomainModel> Arrive(ArriveDTO dto);
}

public class ChargingService : IChargingService
{
    private readonly IChargingRepository _chargingRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IChargingSpotRepository _chargingSpotRepository;
    private readonly IPriceService _priceService;
    private readonly ICardRepository _cardRepository;

    public ChargingService(IChargingRepository chargingRepository, IPriceService priceService, IVehicleRepository vehicleRepository, IChargingSpotRepository chargingSpotRepository, ICardRepository cardRepository)
    {
        _chargingRepository = chargingRepository;
        _vehicleRepository = vehicleRepository;
        _chargingSpotRepository = chargingSpotRepository;
        _priceService = priceService;
        _cardRepository = cardRepository;
    }

    public async Task<List<ChargingDomainModel>> GetAll()
    {
        List<Charging> chargings = await _chargingRepository.GetAll();
        List<ChargingDomainModel> result = new List<ChargingDomainModel>();
        foreach (var item in chargings)
            result.Add(ParseToModel(item));
        return result;
    }

    public async Task<ChargingDomainModel> Arrive(ArriveDTO dto)
    {
        ChargingSpot foundSpot = new ChargingSpot();
        foundSpot = null;
        foreach (var spot in await _chargingSpotRepository.GetAll())
        {
            if (spot.State == 0)
            {
                foundSpot = spot;
                break;
            }
        }

        if (foundSpot == null) return null;

        Charging charging = new Charging
        {
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            CardId = dto.CardId,
            ChargingSpotId = foundSpot.Id,
            IsDeleted = false,
            ReservationId = null,
            UnitPrice = await _priceService.GetPrice(foundSpot.StationId, dto.StartTime)
        };
        decimal duration = (decimal)(dto.EndTime - dto.StartTime).TotalHours;
        Card card = await _cardRepository.GetById(dto.CardId);
        Vehicle vehicle = await _vehicleRepository.GetById(card.VehicleId);
        decimal totalPrice = charging.UnitPrice * duration * vehicle.Power;
        charging.TotalPrice = totalPrice;
        charging.ElectricitySpent = duration;
        Charging newCharging = _chargingRepository.Post(charging);
        _chargingRepository.Save();
        return ParseToModel(charging);
    }

    public static ChargingDomainModel ParseToModel(Charging charging)
    {
        ChargingDomainModel chargingModel = new ChargingDomainModel
        {
            Id = charging.Id,
            IsDeleted = charging.IsDeleted,
            CardId = charging.CardId,
            ChargingSpotId = charging.ChargingSpotId,
            ElectricitySpent = charging.ElectricitySpent,
            EndTime = charging.EndTime,
            StartTime = charging.StartTime,
            ReservationId = charging.ReservationId,
            TotalPrice = charging.TotalPrice,
            UnitPrice = charging.UnitPrice
        };

        // if (charging.Reservation != null)
        //     chargingModel.Reservation = ReservationService.ParseToModel(charging.Reservation);

        return chargingModel;
    }
}