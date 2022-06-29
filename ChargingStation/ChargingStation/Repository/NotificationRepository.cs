using ChargingStation.Data;
using ChargingStation.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChargingStation.Repository;

public interface INotificationRepository : IRepository<Notification>
{
    public Task<List<Notification>> GetByClientId(decimal id);
}

public class NotificationRepository : INotificationRepository
{
    private readonly ChargingStationContext _chargingStationContext;

    public NotificationRepository(ChargingStationContext chargingStationContext)
    {
        _chargingStationContext = chargingStationContext;
    }

    public async Task<List<Notification>> GetAll()
    {
        return await _chargingStationContext.Notifications
            .Where(x => !x.IsRead)
            .ToListAsync();
    }

    public Task<Notification> GetById(string id)
    {
        return null;
    }

    public async Task<Notification> GetById(decimal id)
    {
        return null;
    }

    public async Task<List<Notification>> GetByClientId(decimal id)
    {
        return await _chargingStationContext.Notifications
            .Where(x => !x.IsRead && x.ClientId == id)
            .ToListAsync();
    }

    public void Save()
    {
        _chargingStationContext.SaveChanges();
    }

    public Notification Post(Notification item)
    {
        EntityEntry<Notification> result = _chargingStationContext.Notifications.Add(item);
        return result.Entity;
    }

    public Notification Update(Notification item)
    {
        EntityEntry<Notification> updatedEntry = _chargingStationContext.Notifications.Attach(item);
        _chargingStationContext.Entry(item).State = EntityState.Modified;
        return updatedEntry.Entity;
    }

    public Notification Delete(Notification item)
    {
        return null;
    }
}