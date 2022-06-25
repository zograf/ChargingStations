using ChargingStation.Data;
using ChargingStation.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChargingStation.Repository;

public interface IAddressRepository : IRepository<Address>
{
}

public class AddressRepository : IAddressRepository
{
    private readonly ChargingStationContext _chargingStationContext;

    public AddressRepository(ChargingStationContext chargingStationContext)
    {
        _chargingStationContext = chargingStationContext;
    }
    
    public async Task<List<Address>> GetAll()
    {
        return await _chargingStationContext.Addresses
            .ToListAsync(); 
    }

    public Task<Address> GetById(string id)
    {
        return null;
    }

    public async Task<Address> GetById(decimal id)
    {
        return await _chargingStationContext.Addresses
            .Where(x=>x.Id == id)
            .FirstOrDefaultAsync(); 
    }

    public void Save()
    {
        _chargingStationContext.SaveChanges();
    }

    public Address Post(Address item)
    {
        EntityEntry<Address> result = _chargingStationContext.Addresses.Add(item);
        return result.Entity;
    }

    public Address Update(Address item)
    {
        EntityEntry<Address> updatedEntry = _chargingStationContext.Addresses.Attach(item);
        _chargingStationContext.Entry(item).State = EntityState.Modified;
        return updatedEntry.Entity;
    }

    public Address Delete(Address item)
    {
        item.IsDeleted = true;
        return Update(item);
    }
}