using ChargingStation.Data.Entity;
using ChargingStation.Domain.DTOs;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface ICredentialsService : IService<CredentialsDomainModel>
{
    public Task<UserDomainModel> GetUser(UsernamePasswordDTO dto);
}

public class CredentialsService : ICredentialsService
{
    private readonly ICredentialsRepository _credentialsRepository;
    private readonly IUserRepository _userRepository;

    public CredentialsService(ICredentialsRepository credentialsRepository,
                            IUserRepository userRepository)
    {
        _credentialsRepository = credentialsRepository;
        _userRepository = userRepository;
    }
    
    public async Task<List<CredentialsDomainModel>> GetAll()
    {
        List<Credentials> credentials = await _credentialsRepository.GetAll();
        List<CredentialsDomainModel> result = new List<CredentialsDomainModel>();
        foreach (var item in credentials)
            result.Add(ParseToModel(item));
        return result;
    }

    public async Task<UserDomainModel> GetUser(UsernamePasswordDTO dto)
    {
        Credentials credentials = await _credentialsRepository.GetById(dto.Username);
        if (credentials == null || credentials.Password != dto.Password) throw new UserNotFoundException();
        User user = await _userRepository.GetById(credentials.UserId);
        return UserService.ParseToModel(user);
    }

    public static CredentialsDomainModel ParseToModel(Credentials credentials)
    {
        CredentialsDomainModel credentialsModel = new CredentialsDomainModel
        {
            UserId = credentials.UserId,
            Username = credentials.Username,
            Password = credentials.Password,
            IsDeleted = credentials.IsDeleted
        };
        
        return credentialsModel;
    }
}
