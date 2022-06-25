using ChargingStation.Data;
using ChargingStation.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChargingStation.Repository;

public interface IReservationRepository : IRepository<Reservation>
{
}

public class ReservationRepository : IReservationRepository
{
    private readonly ChargingStationContext _chargingStationContext;

    public ReservationRepository(ChargingStationContext chargingStationContext)
    {
        _chargingStationContext = chargingStationContext;
    }
    
    public async Task<List<Reservation>> GetAll()
    {
        return await _chargingStationContext.Reservations
            .ToListAsync(); 
    }

    public Task<Reservation> GetById(string id)
    {
        return null;
    }

    public async Task<Reservation> GetById(decimal id)
    {
        return await _chargingStationContext.Reservations
            .Where(x=>x.Id == id)
            .FirstOrDefaultAsync(); 
    }

    public void Save()
    {
        _chargingStationContext.SaveChanges();
    }

    public Reservation Post(Reservation item)
    {
        EntityEntry<Reservation> result = _chargingStationContext.Reservations.Add(item);
        return result.Entity;
    }

    public Reservation Update(Reservation item)
    {
        EntityEntry<Reservation> updatedEntry = _chargingStationContext.Reservations.Attach(item);
        _chargingStationContext.Entry(item).State = EntityState.Modified;
        return updatedEntry.Entity;
    }

    public Reservation Delete(Reservation item)
    {
        item.IsDeleted = true;
        return Update(item);
    }
}
