using ChargingStation.Data;
using ChargingStation.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChargingStation.Repository;

public interface IPlaceRepository : IRepository<Place>
{
}

public class PlaceRepository : IPlaceRepository
{
    private readonly ChargingStationContext _chargingStationContext;

    public PlaceRepository(ChargingStationContext chargingStationContext)
    {
        _chargingStationContext = chargingStationContext;
    }

    public async Task<List<Place>> GetAll()
    {
        return await _chargingStationContext.Places
            .Where(x => !x.IsDeleted)
            .ToListAsync();
    }

    public Task<Place> GetById(string id)
    {
        return null;
    }

    public async Task<Place> GetById(decimal id)
    {
        return await _chargingStationContext.Places
            .Where(x => x.Id == id && !x.IsDeleted)
            .FirstOrDefaultAsync();
    }

    public void Save()
    {
        _chargingStationContext.SaveChanges();
    }

    public Place Post(Place item)
    {
        EntityEntry<Place> result = _chargingStationContext.Places.Add(item);
        return result.Entity;
    }

    public Place Update(Place item)
    {
        EntityEntry<Place> updatedEntry = _chargingStationContext.Places.Attach(item);
        _chargingStationContext.Entry(item).State = EntityState.Modified;
        return updatedEntry.Entity;
    }

    public Place Delete(Place item)
    {
        item.IsDeleted = true;
        return Update(item);
    }
}