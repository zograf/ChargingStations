using ChargingStation.Data;
using ChargingStation.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChargingStation.Repository;

public interface IManagerRepository : IRepository<Manager>
{
}

public class ManagerRepository : IManagerRepository
{
    private readonly ChargingStationContext _chargingStationContext;

    public ManagerRepository(ChargingStationContext chargingStationContext)
    {
        _chargingStationContext = chargingStationContext;
    }

    public async Task<List<Manager>> GetAll()
    {
        return await _chargingStationContext.Managers
            .ToListAsync();
    }

    public Task<Manager> GetById(string id)
    {
        return null;
    }

    public async Task<Manager> GetById(decimal id)
    {
        return await _chargingStationContext.Managers
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public void Save()
    {
        _chargingStationContext.SaveChanges();
    }

    public Manager Post(Manager item)
    {
        EntityEntry<Manager> result = _chargingStationContext.Managers.Add(item);
        return result.Entity;
    }

    public Manager Update(Manager item)
    {
        EntityEntry<Manager> updatedEntry = _chargingStationContext.Managers.Attach(item);
        _chargingStationContext.Entry(item).State = EntityState.Modified;
        return updatedEntry.Entity;
    }

    public Manager Delete(Manager item)
    {
        return null;
    }
}