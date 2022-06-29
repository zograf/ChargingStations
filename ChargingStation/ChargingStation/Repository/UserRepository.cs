using ChargingStation.Data;
using ChargingStation.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChargingStation.Repository;

public interface IUserRepository : IRepository<User>
{
}

public class UserRepository : IUserRepository
{
    private readonly ChargingStationContext _chargingStationContext;

    public UserRepository(ChargingStationContext chargingStationContext)
    {
        _chargingStationContext = chargingStationContext;
    }

    public async Task<List<User>> GetAll()
    {
        return await _chargingStationContext.Users
            .Where(x => !x.IsDeleted)
            .Include(x => x.Credentials)
            .ToListAsync();
    }

    public Task<User> GetById(string id)
    {
        return null;
    }

    public async Task<User> GetById(decimal id)
    {
        return await _chargingStationContext.Users
            .Where(x => x.Id == id && !x.IsDeleted)
            .FirstOrDefaultAsync();
    }

    public void Save()
    {
        _chargingStationContext.SaveChanges();
    }

    public User Post(User item)
    {
        EntityEntry<User> result = _chargingStationContext.Users.Add(item);
        return result.Entity;
    }

    public User Update(User item)
    {
        EntityEntry<User> updatedEntry = _chargingStationContext.Users.Attach(item);
        _chargingStationContext.Entry(item).State = EntityState.Modified;
        return updatedEntry.Entity;
    }

    public User Delete(User item)
    {
        item.IsDeleted = true;
        return Update(item);
    }
}