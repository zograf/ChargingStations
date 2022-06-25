using ChargingStation.Data.Entity;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface IVehicleService : IService<VehicleDomainModel>
{
}

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleService(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }
    
    public async Task<List<VehicleDomainModel>> GetAll()
    {
        List<Vehicle> vehicles = await _vehicleRepository.GetAll();
        List<VehicleDomainModel> result = new List<VehicleDomainModel>();
        foreach (var item in vehicles)
            result.Add(ParseToModel(item));
        return result;
    }

    public static VehicleDomainModel ParseToModel(Vehicle vehicle)
    {
        VehicleDomainModel vehicleModel = new VehicleDomainModel
        {
            Id = vehicle.Id,
            IsDeleted = vehicle.IsDeleted,
            ClientId = vehicle.ClientId,
            Name = vehicle.Name,
            RegistrationPlate = vehicle.RegistrationPlate,
            Power = vehicle.Power
        };

        if (vehicle.Card != null)
            vehicleModel.Card = CardService.ParseToModel(vehicle.Card);

        return vehicleModel;
    }
}
