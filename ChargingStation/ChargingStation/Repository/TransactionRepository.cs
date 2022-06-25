using ChargingStation.Data;
using ChargingStation.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChargingStation.Repository;

public interface ITransactionRepository : IRepository<Transaction>
{
}

public class TransactionRepository : ITransactionRepository
{
    private readonly ChargingStationContext _chargingStationContext;

    public TransactionRepository(ChargingStationContext chargingStationContext)
    {
        _chargingStationContext = chargingStationContext;
    }
    
    public async Task<List<Transaction>> GetAll()
    {
        return await _chargingStationContext.Transactions
            .ToListAsync(); 
    }

    public Task<Transaction> GetById(string id)
    {
        return null;
    }

    public async Task<Transaction> GetById(decimal id)
    {
        return await _chargingStationContext.Transactions
            .Where(x=>x.Id == id)
            .FirstOrDefaultAsync(); 
    }

    public void Save()
    {
        _chargingStationContext.SaveChanges();
    }

    public Transaction Post(Transaction item)
    {
        EntityEntry<Transaction> result = _chargingStationContext.Transactions.Add(item);
        return result.Entity;
    }

    public Transaction Update(Transaction item)
    {
        EntityEntry<Transaction> updatedEntry = _chargingStationContext.Transactions.Attach(item);
        _chargingStationContext.Entry(item).State = EntityState.Modified;
        return updatedEntry.Entity;
    }

    public Transaction Delete(Transaction item)
    {
        item.IsDeleted = true;
        return Update(item);
    }
}
