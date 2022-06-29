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
        if (dto.StartTime >= dto.EndTime)
            throw new Exception("Start time is after end time");
        if (dto.StartTime < DateTime.Now)
             throw new Exception("Start time is in the past");
        Charging charging = await FindAvaliableCharging(dto.StartTime, dto.EndTime, dto.CardId);
        if (charging is null)
            throw new Exception("Cannot do charging, no avaliable slots");
        charging = _chargingRepository.Post(charging);
        _chargingRepository.Save();
        return ParseToModel(charging);
    }

    private async Task<Charging> FindAvaliableCharging(DateTime start, DateTime end, decimal cardId)
    {
        IEnumerable<ChargingSpotDomainModel> spots = await _chargingSpotService.GetForSuddenArrival();
        foreach (ChargingSpotDomainModel spot in spots)
        {
            if (spot.State == 0)
            {
                decimal duration = (decimal)(end - start).TotalHours;
                decimal price = await _priceService.GetPrice(spot.StationId, start);
                return new Charging
                {
                    ElectricitySpent = duration,
                    TotalPrice = duration * price,
                    StartTime = start,
                    EndTime = end,
                    ChargingSpotId = spot.Id,
                    CardId = cardId,
                    IsDeleted = false,
                    UnitPrice = price,
                };
            }

        }
        return null;
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