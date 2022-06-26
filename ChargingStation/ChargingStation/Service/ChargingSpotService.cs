using ChargingStation.Data.Entity;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface IChargingSpotService : IService<ChargingSpotDomainModel>
{
}

public class ChargingSpotService : IChargingSpotService
{
    private readonly IChargingSpotRepository _chargingSpotRepository;

    public ChargingSpotService(IChargingSpotRepository chargingSpotRepository)
    {
        _chargingSpotRepository = chargingSpotRepository;
    }
    
    public async Task<List<ChargingSpotDomainModel>> GetAll()
    {
        List<ChargingSpot> chargingSpots = await _chargingSpotRepository.GetAll();
        List<ChargingSpotDomainModel> result = new List<ChargingSpotDomainModel>();
        foreach (var item in chargingSpots)
            result.Add(ParseToModel(item));
        return result;
    }

    public static ChargingSpotDomainModel ParseToModel(ChargingSpot chargingSpot)
    {
        ChargingSpotDomainModel chargingSpotModel = new ChargingSpotDomainModel
        {
            Id = chargingSpot.Id,
            IsDeleted = chargingSpot.IsDeleted,
            IsReservable = chargingSpot.IsReservable,
            StationId = chargingSpot.StationId,
            SerialNumber = chargingSpot.SerialNumber,
            State = chargingSpot.State
        };

        chargingSpotModel.Chargings = new List<ChargingDomainModel>();
        if (chargingSpot.Chargings != null)
            foreach (var item in chargingSpot.Chargings)
                chargingSpotModel.Chargings.Add(ChargingService.ParseToModel(item));
        
        return chargingSpotModel;
    }
}
