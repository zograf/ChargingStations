using ChargingStation.Data;
using ChargingStation.Data.Entity;
using ChargingStation.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChargingStation.Repository;

public interface IBasePriceRepository : IRepository<BasePrice>
{
}

public class BasePriceRepository : IBasePriceRepository
{
    private readonly ChargingStationContext _chargingStationContext;

    public BasePriceRepository(ChargingStationContext chargingStationContext)
    {
        _chargingStationContext = chargingStationContext;
    }
    
    public async Task<List<BasePrice>> GetAll()
    {
        return await _chargingStationContext.BasePrices
            .Where(x=>!x.IsDeleted)
            .ToListAsync(); 
    }
    
    public Task<BasePrice> GetById(string id)
    {
        return null;
    }

    public async Task<BasePrice> GetById(decimal id)
    {
        return await _chargingStationContext.BasePrices
            .Where(x=>x.Id == id && !x.IsDeleted)
            .FirstOrDefaultAsync(); 
    }

    public void Save()
    {
        _chargingStationContext.SaveChanges();
    }

    public BasePrice Post(BasePrice item)
    {
        EntityEntry<BasePrice> result = _chargingStationContext.BasePrices.Add(item);
        return result.Entity;
    }

    public BasePrice Update(BasePrice item)
    {
        EntityEntry<BasePrice> updatedEntry = _chargingStationContext.BasePrices.Attach(item);
        _chargingStationContext.Entry(item).State = EntityState.Modified;
        return updatedEntry.Entity;
    }

    public BasePrice Delete(BasePrice item)
    {
        item.IsDeleted = true;
        return Update(item);
    }
}
