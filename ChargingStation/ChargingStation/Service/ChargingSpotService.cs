using ChargingStation.Data.Entity;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface IChargingSpotService : IService<ChargingSpotDomainModel>
{
    public Task<decimal> GetState(int id);

    public Task ChangeState(decimal id, decimal state);

    public Task<Boolean> ManageStates();

    Task<IEnumerable<ChargingSpotDomainModel>> GetForReservations();

    Task<IEnumerable<ChargingSpotDomainModel>> GetForSuddenArrival();
}

public class ChargingSpotService : IChargingSpotService
{
    private readonly IChargingSpotRepository _chargingSpotRepository;

    public ChargingSpotService(IChargingSpotRepository chargingSpotRepository)
    {
        _chargingSpotRepository = chargingSpotRepository;
    }

    public async Task<Boolean> ManageStates()
    {
        List<ChargingSpot> chargingSpots = await _chargingSpotRepository.GetAll();
        foreach (var item in chargingSpots)
        {
            if (item.State == 3)
            {
                continue;
            }
            item.State = 0;
            foreach (var reservation in item.Reservations)
            {
                if (reservation.IsDeleted) continue;
                if (reservation.StartTime < DateTime.Now && reservation.EndTime > DateTime.Now)
                    item.State = 1;
            }
            foreach (var charging in item.Chargings)
                if (charging.StartTime < DateTime.Now && charging.EndTime > DateTime.Now)
                    item.State = 2;

            _chargingSpotRepository.Update(item);
        }
        _chargingSpotRepository.Save();
        return true;
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

    public async Task<IEnumerable<ChargingSpotDomainModel>> GetForReservations()
    {
        IEnumerable<ChargingSpot> chargingSpots = await _chargingSpotRepository.GeForReservation();
        return ParseToModel(chargingSpots);
    }

    private IEnumerable<ChargingSpotDomainModel> ParseToModel(IEnumerable<ChargingSpot> chargingSpots)
    {
        return chargingSpots.Select(x => ParseToModel(x));
    }

    public async Task<IEnumerable<ChargingSpotDomainModel>> GetForSuddenArrival()
    {
        IEnumerable<ChargingSpot> chargingSpots = await _chargingSpotRepository.GetForSuddenArrival();
        return ParseToModel(chargingSpots);
    }

    public async Task<decimal> GetState(int id)
    {
        var spot = await _chargingSpotRepository.GetById(id);
        return spot.State;
    }

    public async Task ChangeState(decimal id, decimal state)
    {
        var spot = await _chargingSpotRepository.GetById(id);
        spot.State = state;
        _chargingSpotRepository.Update(spot);
        _chargingSpotRepository.Save();
    }
}