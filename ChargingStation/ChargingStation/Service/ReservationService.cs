using ChargingStation.Data.Entity;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface IReservationService : IService<ReservationDomainModel>
{
    public Task<List<ClientDomainModel>> CheckValidity();
}

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }
    
    public async Task<List<ReservationDomainModel>> GetAll()
    {
        List<Reservation> reservations = await _reservationRepository.GetAll();
        List<ReservationDomainModel> result = new List<ReservationDomainModel>();
        foreach (var item in reservations)
            result.Add(ParseToModel(item));
        return result;
    }
    
    public async Task<List<ClientDomainModel>> CheckValidity()
    {
        return new List<ClientDomainModel>();
    }

    public static ReservationDomainModel ParseToModel(Reservation reservation)
    {
        ReservationDomainModel reservationModel = new ReservationDomainModel
        {
            Id = reservation.Id,
            IsDeleted = reservation.IsDeleted,
            CardId = reservation.CardId,
            ChargingId = reservation.CardId,
            EndTime = reservation.EndTime,
            StartTime = reservation.StartTime,
            UnitPrice = reservation.UnitPrice
        };

        return reservationModel;
    }
}
