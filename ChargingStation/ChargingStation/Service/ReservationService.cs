using ChargingStation.Data.Entity;
using ChargingStation.Domain.DTOs;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface IReservationService : IService<ReservationDomainModel>
{
    public Task<List<ClientDomainModel>> CheckValidity();
    public Task<ReservationDomainModel> CreateReservation(ReservationDTO dto);
}

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IChargingSpotService _chargingSpotService;
    private readonly IPriceService _priceService;

    public ReservationService(IReservationRepository reservationRepository,
        IChargingSpotService chargingSpotService,
        IPriceService priceService)
    {
        _reservationRepository = reservationRepository;
        _chargingSpotService = chargingSpotService;
        _priceService = priceService;
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

    public async Task<ReservationDomainModel> CreateReservation(ReservationDTO dto)
    {
        Reservation reservation = await FindReservation(dto.StartTime, dto.EndTime, dto.CardId);
        if (reservation is null)
            throw new Exception("Cannot appoint reservation in that time, no available slots");
        _reservationRepository.Post(reservation);
        _reservationRepository.Save();
        return ParseToModel(reservation);
    }

    private async Task<Reservation> FindReservation(DateTime start, DateTime end, decimal cardId)
    {
        IEnumerable<ChargingSpotDomainModel> spots = await _chargingSpotService.GetAll();
        foreach(ChargingSpotDomainModel spot in spots)
        {
            if(! await OverlapsReservations(start, end, spot))
                return new Reservation
                {
                    StartTime = start,
                    EndTime = end,
                    ChargingSpotId = spot.Id,
                    CardId = cardId,
                    IsDeleted = false,
                    UnitPrice = await _priceService.GetPrice(spot.StationId, start)
                };
        }
        return null;
    }

    private async Task<bool> OverlapsReservations(DateTime start, DateTime end, ChargingSpotDomainModel spot)
    {
        IEnumerable<ReservationDomainModel> reservations = await GetByChargingSpot(spot);
        foreach (ReservationDomainModel reservation in reservations)
        {
            if (reservation.IsOverlaping(start, end, 15))
            {
                return true;
            }
        }
        return false;
    }

    private async Task<IEnumerable<ReservationDomainModel>> GetByChargingSpot(ChargingSpotDomainModel spot)
    {
        List<Reservation> reservations = await _reservationRepository.GetByChargingSpot(spot.Id);
        return ParseToModel(reservations);
    }

    private IEnumerable<ReservationDomainModel> ParseToModel(List<Reservation> reservations)
    {
        return reservations.Select(reservation => ParseToModel(reservation));
    }
}
