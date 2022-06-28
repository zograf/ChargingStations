using ChargingStation.Data;
using ChargingStation.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChargingStation.Repository;

public interface IClientRepository : IRepository<Client>
{
    public Task<Client> GetByUserId(decimal id);
}

public class ClientRepository : IClientRepository
{
    private readonly ChargingStationContext _chargingStationContext;

    public ClientRepository(ChargingStationContext chargingStationContext)
    {
        _chargingStationContext = chargingStationContext;
    }
    
    public async Task<List<Client>> GetAll()
    {
        return await _chargingStationContext.Clients
            .ToListAsync(); 
    }

    public Task<Client> GetById(string id)
    {
        return null;
    }
    
    public async Task<Client> GetById(decimal id)
    {
        return await _chargingStationContext.Clients
            .Where(x=>x.Id == id)
            .Include(x=>x.Vehicles)
            .ThenInclude(y=>y.Card)
            .ThenInclude(z=>z.Reservations)
            .FirstOrDefaultAsync(); 
    }
    
    public async Task<Client> GetByUserId(decimal id)
    {
        return await _chargingStationContext.Clients
            .Where(x=>x.UserId == id)
            .Include(x=>x.Vehicles)
            .ThenInclude(y=>y.Card)
            .ThenInclude(z=>z.Reservations)
            .FirstOrDefaultAsync(); 
    }

    public void Save()
    {
        _chargingStationContext.SaveChanges();
    }

    public Client Post(Client item)
    {
        EntityEntry<Client> result = _chargingStationContext.Clients.Add(item);
        return result.Entity;
    }

    public Client Update(Client item)
    {
        EntityEntry<Client> updatedEntry = _chargingStationContext.Clients.Attach(item);
        _chargingStationContext.Entry(item).State = EntityState.Modified;
        return updatedEntry.Entity;
    }

    public Client Delete(Client item)
    {
        return null;
    }
}
