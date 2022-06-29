using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface ISimulationService
{
    public void SimulationIteration();
}

public class SimulationService : ISimulationService
{
    private readonly IChargingSpotService _chargingSpotService;
    private readonly IReservationService _reservationService;
    private readonly IChargingService _chargingService;
    private readonly IChargingRepository _chargingRepository;
    private readonly ICardService _cardService;

    public SimulationService(IChargingSpotService chargingSpotService, IReservationService reservationService, IChargingService chargingService, IChargingRepository chargingRepository, ICardService cardService)
    {
        this._chargingSpotService = chargingSpotService;
        this._reservationService = reservationService;
        this._chargingService = chargingService;
        this._chargingRepository = chargingRepository;
        this._cardService = cardService;
    }

    public async void SimulationIteration()
    {
        Repair();
        Malfunction();
        Arrive();
    }

    private async void Arrive()
    {
        Random rand = new Random(Guid.NewGuid().GetHashCode());
        List<CardDomainModel> cards = await _cardService.GetAll();
        int cardId = rand.Next(0, cards.Count);

        for (int i = 0; i < 2; i++)
        {
            var dto = new Domain.DTOs.ArriveDTO();
            dto.CardId = cardId;
            dto.StartTime = DateTime.Now;
            dto.EndTime = DateTime.Now.AddSeconds(120);
            _chargingService.Arrive(dto);
        }
    }

    private async void Repair()
    {
        List<ChargingSpotDomainModel> spots = await _chargingSpotService.GetAll();
        foreach (var spot in spots)
        {
            if (spot.State == 3)
            {
                _chargingSpotService.ChangeState(spot.Id, 3);
                break;
            }
        }
    }

    private async void Malfunction()
    {
        List<ChargingSpotDomainModel> spots = await _chargingSpotService.GetAll();
        var count = spots.Count;
        Random rand = new Random(Guid.NewGuid().GetHashCode());
        var spotId = rand.Next(count);
        var state = await _chargingSpotService.GetState(spotId);
        if (state == 0)
        {
            _chargingSpotService.ChangeState(spotId, 3);
        }
        else if (state == 1)
        {
            CancelReservationsAsync(spotId);
        }
        else if (state == 2)
        {
            FinishCharging(spotId);
        }

        _chargingSpotService.ChangeState(spotId, 3);
    }

    private async void CancelReservationsAsync(decimal spotId)
    {
        List<ReservationDomainModel> reservations = await _reservationService.GetAll();
        foreach (var reservation in reservations)
        {
            if (reservation.ChargingSpotId == spotId && reservation.StartTime.Date == DateTime.Today)
            {
                _reservationService.Cancel(reservation.Id);
            }
        }
    }

    private async void FinishCharging(decimal spotId)
    {
        List<ChargingDomainModel> chargings = await _chargingService.GetAll();
        foreach (var chaging in chargings)
        {
            if (chaging.ChargingSpotId == spotId && chaging.StartTime <= DateTime.Now && chaging.EndTime >= DateTime.Now)
            {
                chaging.EndTime = DateTime.Now;
                break;
            }
        }
    }
}