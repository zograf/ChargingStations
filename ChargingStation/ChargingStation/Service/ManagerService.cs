using ChargingStation.Data.Entity;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface IManagerService : IService<ManagerDomainModel>
{
}

public class ManagerService : IManagerService
{
    private readonly IManagerRepository _managerRepository;

    public ManagerService(IManagerRepository managerRepository)
    {
        _managerRepository = managerRepository;
    }

    public async Task<List<ManagerDomainModel>> GetAll()
    {
        List<Manager> managers = await _managerRepository.GetAll();
        List<ManagerDomainModel> result = new List<ManagerDomainModel>();
        foreach (var item in managers)
            result.Add(ParseToModel(item));
        return result;
    }

    public static ManagerDomainModel ParseToModel(Manager manager)
    {
        ManagerDomainModel managerModel = new ManagerDomainModel
        {
            UserId = manager.UserId,
            Id = manager.Id,
            StationId = manager.StationId
        };

        if (manager.User != null)
            managerModel.User = UserService.ParseToModel(manager.User);

        return managerModel;
    }
}