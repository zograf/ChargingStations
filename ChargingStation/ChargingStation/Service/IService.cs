namespace ChargingStation.Service;

public interface IService<T> where T : class 
{
    Task<List<T>> GetAll();
}