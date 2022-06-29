using ChargingStation.Data.Entity;
using ChargingStation.Domain.DTOs;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;
using Microsoft.Data.SqlClient;

namespace ChargingStation.Service;

public interface IReservationService : IService<ReservationDomainModel>
{
    public Task<List<ClientDomainModel>> CheckValidity();
    public Task<ReservationDomainModel> CreateReservation(ReservationDTO dto);
    Task<IEnumerable<Tuple<DateTime, DateTime>>> GetReservedTimeSlots(decimal slotId);
    public Task<ReservationDomainModel> Cancel(decimal id);
}

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IChargingSpotService _chargingSpotService;
    private readonly IPriceService _priceService;
    private readonly IClientRepository _clientRepository;
    private readonly INotificationRepository _notificationRepository;
    private readonly IChargingSpotRepository _chargingSpotRepository;

    public ReservationService(IReservationRepository reservationRepository,
        IChargingSpotService chargingSpotService,
        IPriceService priceService,
        IClientRepository clientRepository,
        INotificationRepository notificationRepository,
        IChargingSpotRepository chargingSpotRepository)
    {
        _reservationRepository = reservationRepository;
        _chargingSpotService = chargingSpotService;
        _priceService = priceService;
        _clientRepository = clientRepository;
        _notificationRepository = notificationRepository;
        _chargingSpotRepository = chargingSpotRepository;
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
        List<Client> clients = await _clientRepository.GetAll();
        List<Client> result = new List<Client>();
        DateTime now = DateTime.Now;
        foreach (var client in clients)
        {
            foreach (var vehicle in client.Vehicles)
            {
                if (vehicle.IsDeleted || vehicle.Card.IsDeleted) continue;
                foreach (var reservation in vehicle.Card.Reservations)
                {
                    if (reservation.IsDeleted) continue;
                    if (reservation.ChargingId != null) continue;
                    if (reservation.StartTime < now)
                    {
                        await Cancel(reservation.Id);
                        ChargingSpot cs = await _chargingSpotRepository.GetByReservationId(reservation.Id);
                        if (cs.State == 1)
                        {
                            cs.State = 0;
                            _chargingSpotRepository.Update(cs);
                            _chargingSpotRepository.Save();
                        }
                        if (!result.Contains(client)) 
                            result.Add(client);
                    }
                }
            }
        }
        List<ClientDomainModel> ret = new List<ClientDomainModel>();
        foreach (var item in result)
        {
            ret.Add(ClientService.ParseToModel(item));
            _ = SendNotification(item.UserId);
        }
        return ret;
    }

    public bool SendNotification(decimal id)
    {
        Notification notification = new Notification
        {
            ClientId = id,
            IsRead = false
        };
        _notificationRepository.Post(notification);
        _notificationRepository.Save();
        return true;
    }

    public async Task<ReservationDomainModel> Cancel(decimal id)
    {
        Reservation reservation = await _reservationRepository.GetById(id);
        _reservationRepository.Delete(reservation);
        _reservationRepository.Save();
        return ParseToModel(reservation);
    }

    public static ReservationDomainModel ParseToModel(Reservation reservation)
    {
        ReservationDomainModel reservationModel = new ReservationDomainModel
        {
            Id = reservation.Id,
            IsDeleted = reservation.IsDeleted,
            CardId = reservation.CardId,
            ChargingId = reservation.CardId,
            ChargingSpotId = reservation.ChargingSpotId,
            EndTime = reservation.EndTime,
            StartTime = reservation.StartTime,
            UnitPrice = reservation.UnitPrice
        };

        return reservationModel;
    }

    public async Task<ReservationDomainModel> CreateReservation(ReservationDTO dto)
    {
        if (dto.StartTime >= dto.EndTime)
            throw new Exception("Start time is after end time");
        if (dto.StartTime < DateTime.Now)
            throw new Exception("Start time is in the past");
        Reservation reservation = await FindReservation(dto.StartTime, dto.EndTime, dto.CardId);
        if (reservation is null)
            throw new Exception("Cannot appoint reservation in that time, no available slots");
        reservation = _reservationRepository.Post(reservation);
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
        IEnumerable<Reservation> reservations = await _reservationRepository.GetByChargingSpot(spot.Id);
        return ParseToModel(reservations);
    }

    private IEnumerable<ReservationDomainModel> ParseToModel(IEnumerable<Reservation> reservations)
    {
        return reservations.Select(reservation => ParseToModel(reservation));
    }

    public async Task<IEnumerable<Tuple<DateTime, DateTime>>> GetReservedTimeSlots(decimal slotId)
    {
        IEnumerable<Reservation> reservations = await _reservationRepository.GetByChargingSpot(slotId);
        return reservations.Select(x => new Tuple<DateTime, DateTime>(x.StartTime, x.EndTime));
    }
}
