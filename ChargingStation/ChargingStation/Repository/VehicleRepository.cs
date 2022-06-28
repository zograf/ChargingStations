using ChargingStation.Data;
using ChargingStation.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChargingStation.Repository;

public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<IEnumerable<Vehicle>> GetByClient(decimal clientId);
}

public class VehicleRepository : IVehicleRepository
{
    private readonly ChargingStationContext _chargingStationContext;

    public VehicleRepository(ChargingStationContext chargingStationContext)
    {
        _chargingStationContext = chargingStationContext;
    }
    
    public async Task<List<Vehicle>> GetAll()
    {
        return await _chargingStationContext.Vehicles
            .Where(x=>!x.IsDeleted)
            .ToListAsync(); 
    }

    public Task<Vehicle> GetById(string id)
    {
        return null;
    }

    public async Task<Vehicle> GetById(decimal id)
    {
        return await _chargingStationContext.Vehicles
            .Where(x=>x.Id == id && !x.IsDeleted)
            .FirstOrDefaultAsync(); 
    }

    public void Save()
    {
        _chargingStationContext.SaveChanges();
    }

    public Vehicle Post(Vehicle item)
    {
        EntityEntry<Vehicle> result = _chargingStationContext.Vehicles.Add(item);
        return result.Entity;
    }

    public Vehicle Update(Vehicle item)
    {
        EntityEntry<Vehicle> updatedEntry = _chargingStationContext.Vehicles.Attach(item);
        _chargingStationContext.Entry(item).State = EntityState.Modified;
        return updatedEntry.Entity;
    }

    public Vehicle Delete(Vehicle item)
    {
        item.IsDeleted = true;
        return Update(item);
    }

    public async Task<IEnumerable<Vehicle>> GetByClient(decimal clientId)
    {
        return await _chargingStationContext.Vehicles.Where(x => x.ClientId == clientId).ToListAsync();
    }
}
