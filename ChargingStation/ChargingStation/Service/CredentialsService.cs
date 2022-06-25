using ChargingStation.Data.Entity;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface ICredentialsService : IService<CredentialsDomainModel>
{
}

public class CredentialsService : ICredentialsService
{
    private readonly ICredentialsRepository _credentialsRepository;

    public CredentialsService(ICredentialsRepository credentialsRepository)
    {
        _credentialsRepository = credentialsRepository;
    }
    
    public async Task<List<CredentialsDomainModel>> GetAll()
    {
        List<Credentials> credentials = await _credentialsRepository.GetAll();
        List<CredentialsDomainModel> result = new List<CredentialsDomainModel>();
        foreach (var item in credentials)
            result.Add(ParseToModel(item));
        return result;
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
