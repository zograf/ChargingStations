using ChargingStation.Data.Entity;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface IChargingService : IService<ChargingDomainModel>
{
}

public class ChargingService : IChargingService
{
    private readonly IChargingRepository _chargingRepository;

    public ChargingService(IChargingRepository chargingRepository)
    {
        _chargingRepository = chargingRepository;
    }
    
    public async Task<List<ChargingDomainModel>> GetAll()
    {
        List<Charging> chargings = await _chargingRepository.GetAll();
        List<ChargingDomainModel> result = new List<ChargingDomainModel>();
        foreach (var item in chargings)
            result.Add(ParseToModel(item));
        return result;
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

        if (charging.Reservation != null)
            chargingModel.Reservation = ReservationService.ParseToModel(charging.Reservation);
        
        return chargingModel;
    }
}
