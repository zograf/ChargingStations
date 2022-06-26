using ChargingStation.Data;
using ChargingStation.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChargingStation.Repository;

public interface IChargingSpotRepository : IRepository<ChargingSpot>
{
}

public class ChargingSpotRepository : IChargingSpotRepository
{
    private readonly ChargingStationContext _chargingStationContext;

    public ChargingSpotRepository(ChargingStationContext chargingStationContext)
    {
        _chargingStationContext = chargingStationContext;
    }
    
    public async Task<List<ChargingSpot>> GetAll()
    {
        return await _chargingStationContext.ChargingSpots
            .Where(x=>!x.IsDeleted)
            .ToListAsync(); 
    }

    public Task<ChargingSpot> GetById(string id)
    {
        return null;
    }

    public async Task<ChargingSpot> GetById(decimal id)
    {
        return await _chargingStationContext.ChargingSpots
            .Where(x=>x.Id == id && !x.IsDeleted)
            .FirstOrDefaultAsync(); 
    }

    public void Save()
    {
        _chargingStationContext.SaveChanges();
    }

    public ChargingSpot Post(ChargingSpot item)
    {
        EntityEntry<ChargingSpot> result = _chargingStationContext.ChargingSpots.Add(item);
        return result.Entity;
    }

    public ChargingSpot Update(ChargingSpot item)
    {
        EntityEntry<ChargingSpot> updatedEntry = _chargingStationContext.ChargingSpots.Attach(item);
        _chargingStationContext.Entry(item).State = EntityState.Modified;
        return updatedEntry.Entity;
    }

    public ChargingSpot Delete(ChargingSpot item)
    {
        item.IsDeleted = true;
        return Update(item);
    }
}
