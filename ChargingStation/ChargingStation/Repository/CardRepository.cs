using ChargingStation.Data;
using ChargingStation.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChargingStation.Repository;

public interface ICardRepository : IRepository<Card>
{
}

public class CardRepository : ICardRepository
{
    private readonly ChargingStationContext _chargingStationContext;

    public CardRepository(ChargingStationContext chargingStationContext)
    {
        _chargingStationContext = chargingStationContext;
    }
    
    public async Task<List<Card>> GetAll()
    {
        return await _chargingStationContext.Cards
            .ToListAsync(); 
    }

    public Task<Card> GetById(string id)
    {
        return null;
    }

    public async Task<Card> GetById(decimal id)
    {
        return await _chargingStationContext.Cards
            .Where(x=>x.Id == id)
            .FirstOrDefaultAsync(); 
    }

    public void Save()
    {
        _chargingStationContext.SaveChanges();
    }

    public Card Post(Card item)
    {
        EntityEntry<Card> result = _chargingStationContext.Cards.Add(item);
        return result.Entity;
    }

    public Card Update(Card item)
    {
        EntityEntry<Card> updatedEntry = _chargingStationContext.Cards.Attach(item);
        _chargingStationContext.Entry(item).State = EntityState.Modified;
        return updatedEntry.Entity;
    }

    public Card Delete(Card item)
    {
        item.IsDeleted = true;
        return Update(item);
    }
}
