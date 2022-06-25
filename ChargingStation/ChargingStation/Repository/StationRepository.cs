using ChargingStation.Data;
using ChargingStation.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChargingStation.Repository;

public interface IStationRepository : IRepository<Station>
{
}

public class StationRepository : IStationRepository
{
    private readonly ChargingStationContext _chargingStationContext;

    public StationRepository(ChargingStationContext chargingStationContext)
    {
        _chargingStationContext = chargingStationContext;
    }
    
    public async Task<List<Station>> GetAll()
    {
        return await _chargingStationContext.Stations
            .ToListAsync(); 
    }

    public Task<Station> GetById(string id)
    {
        return null;
    }

    public async Task<Station> GetById(decimal id)
    {
        return await _chargingStationContext.Stations
            .Where(x=>x.Id == id)
            .FirstOrDefaultAsync(); 
    }

    public void Save()
    {
        _chargingStationContext.SaveChanges();
    }

    public Station Post(Station item)
    {
        EntityEntry<Station> result = _chargingStationContext.Stations.Add(item);
        return result.Entity;
    }

    public Station Update(Station item)
    {
        EntityEntry<Station> updatedEntry = _chargingStationContext.Stations.Attach(item);
        _chargingStationContext.Entry(item).State = EntityState.Modified;
        return updatedEntry.Entity;
    }

    public Station Delete(Station item)
    {
        item.IsDeleted = true;
        return Update(item);
    }
}
