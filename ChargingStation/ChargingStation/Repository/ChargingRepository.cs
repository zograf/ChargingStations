using ChargingStation.Data;
using ChargingStation.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChargingStation.Repository;

public interface IChargingRepository : IRepository<Charging>
{
}

public class ChargingRepository : IChargingRepository
{
    private readonly ChargingStationContext _chargingStationContext;

    public ChargingRepository(ChargingStationContext chargingStationContext)
    {
        _chargingStationContext = chargingStationContext;
    }
    
    public async Task<List<Charging>> GetAll()
    {
        return await _chargingStationContext.Chargings
            .Where(x=>!x.IsDeleted)
            .ToListAsync(); 
    }
    
    public Task<Charging> GetById(string id)
    {
        return null;
    }

    public async Task<Charging> GetById(decimal id)
    {
        return await _chargingStationContext.Chargings
            .Where(x=>x.Id == id && !x.IsDeleted)
            .FirstOrDefaultAsync(); 
    }

    public void Save()
    {
        _chargingStationContext.SaveChanges();
    }

    public Charging Post(Charging item)
    {
        EntityEntry<Charging> result = _chargingStationContext.Chargings.Add(item);
        return result.Entity;
    }

    public Charging Update(Charging item)
    {
        EntityEntry<Charging> updatedEntry = _chargingStationContext.Chargings.Attach(item);
        _chargingStationContext.Entry(item).State = EntityState.Modified;
        return updatedEntry.Entity;
    }

    public Charging Delete(Charging item)
    {
        item.IsDeleted = true;
        return Update(item);
    }
}
