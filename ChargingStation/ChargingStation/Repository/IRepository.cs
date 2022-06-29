namespace ChargingStation.Repository;

public interface IRepository<T> where T : class
{
    public Task<List<T>> GetAll();

    public Task<T> GetById(decimal id);

    public Task<T> GetById(string id);

    public void Save();

    public T Post(T item);

    public T Update(T item);

    public T Delete(T item);
}