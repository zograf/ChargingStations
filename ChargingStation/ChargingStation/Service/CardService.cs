using ChargingStation.Data.Entity;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface ICardService : IService<CardDomainModel>
{
}

public class CardService : ICardService
{
    private readonly ICardRepository _cardRepository;

    public CardService(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public async Task<List<CardDomainModel>> GetAll()
    {
        List<Card> cards = await _cardRepository.GetAll();
        List<CardDomainModel> result = new List<CardDomainModel>();
        foreach (var item in cards)
            result.Add(ParseToModel(item));
        return result;
    }

    public static CardDomainModel ParseToModel(Card card)
    {
        CardDomainModel cardModel = new CardDomainModel
        {
            Id = card.Id,
            IsDeleted = card.IsDeleted,
            IsBlocked = card.IsBlocked,
            NotComingCounter = card.NotComingCounter,
            StayedLongerCounter = card.StayedLongerCounter,
            VehicleId = card.VehicleId
        };

        cardModel.Reservations = new List<ReservationDomainModel>();
        if (card.Reservations != null)
            foreach (var item in card.Reservations)
                cardModel.Reservations.Add(ReservationService.ParseToModel(item));

        cardModel.Chargings = new List<ChargingDomainModel>();
        if (card.Chargings != null)
            foreach (var item in card.Chargings)
                cardModel.Chargings.Add(ChargingService.ParseToModel(item));

        return cardModel;
    }
}