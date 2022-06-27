namespace ChargingStation.Service;

public interface IService<T> where T : class 
{
    Task<List<T>> GetAll();
}

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("User with that username and password was not found")
    {
    }
}