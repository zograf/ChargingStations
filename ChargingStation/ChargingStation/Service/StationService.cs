using ChargingStation.Data.Entity;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface IStationService : IService<StationDomainModel>
{
}

public class StationService : IStationService
{
    private readonly IStationRepository _stationRepository;

    public StationService(IStationRepository stationRepository)
    {
        _stationRepository = stationRepository;
    }

    public async Task<List<StationDomainModel>> GetAll()
    {
        List<Station> stations = await _stationRepository.GetAll();
        List<StationDomainModel> result = new List<StationDomainModel>();
        foreach (var item in stations)
            result.Add(ParseToModel(item));
        return result;
    }

    public static StationDomainModel ParseToModel(Station station)
    {
        StationDomainModel stationModel = new StationDomainModel
        {
            Id = station.Id,
            IsDeleted = station.IsDeleted,
            Name = station.Name
        };

        if (station.Manager != null)
            stationModel.Manager = ManagerService.ParseToModel(station.Manager);

        if (station.Address != null)
            stationModel.Address = AddressService.ParseToModel(station.Address);

        stationModel.BasePrices = new List<BasePriceDomainModel>();
        if (station.BasePrices != null)
            foreach (var item in station.BasePrices)
                stationModel.BasePrices.Add(PriceService.ParseToModel(item));

        stationModel.ChargingSpots = new List<ChargingSpotDomainModel>();
        if (station.ChargingSpots != null)
            foreach (var item in station.ChargingSpots)
                stationModel.ChargingSpots.Add(ChargingSpotService.ParseToModel(item));

        return stationModel;
    }
}