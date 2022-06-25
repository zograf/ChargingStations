using ChargingStation.Data.Entity;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface IUserService : IService<UserDomainModel>
{
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<List<UserDomainModel>> GetAll()
    {
        List<User> users = await _userRepository.GetAll();
        List<UserDomainModel> result = new List<UserDomainModel>();
        foreach (var item in users)
            result.Add(ParseToModel(item));
        return result;
    }

    public static UserDomainModel ParseToModel(User user)
    {
        UserDomainModel userModel = new UserDomainModel
        {
            Id = user.Id,
            IsDeleted = user.IsDeleted,
            Role = user.Role
        };

        if (user.Credentials != null)
            userModel.Credentials = CredentialsService.ParseToModel(user.Credentials);

        return userModel;
    }
}
