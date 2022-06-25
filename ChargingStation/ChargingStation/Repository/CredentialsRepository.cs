using ChargingStation.Data;
using ChargingStation.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChargingStation.Repository;

public interface ICredentialsRepository : IRepository<Credentials>
{
}

public class CredentialsRepository : ICredentialsRepository
{
    private readonly ChargingStationContext _chargingStationContext;

    public CredentialsRepository(ChargingStationContext chargingStationContext)
    {
        _chargingStationContext = chargingStationContext;
    }
    
    public async Task<List<Credentials>> GetAll()
    {
        return await _chargingStationContext.Credentials
            .ToListAsync(); 
    }

    public Task<Credentials> GetById(decimal id)
    {
        return null;
    }

    public async Task<Credentials> GetById(string id)
    {
        return await _chargingStationContext.Credentials
            .Where(x=>x.Username == id)
            .FirstOrDefaultAsync(); 
    }

    public void Save()
    {
        _chargingStationContext.SaveChanges();
    }

    public Credentials Post(Credentials item)
    {
        EntityEntry<Credentials> result = _chargingStationContext.Credentials.Add(item);
        return result.Entity;
    }

    public Credentials Update(Credentials item)
    {
        EntityEntry<Credentials> updatedEntry = _chargingStationContext.Credentials.Attach(item);
        _chargingStationContext.Entry(item).State = EntityState.Modified;
        return updatedEntry.Entity;
    }

    public Credentials Delete(Credentials item)
    {
        return null;
    }
}
