using ChargingStation.Data.Entity;
using ChargingStation.Domain.DTOs;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface IVehicleService : IService<VehicleDomainModel>
{
    public Task<VehicleDomainModel> Create(VehicleDTO dto);
    public Task<IEnumerable<VehicleDomainModel>> GetByClient(decimal clientId);
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

    public async Task<VehicleDomainModel> Create(VehicleDTO dto)
    {
        if (!new List<int> { 250, 100, 55, 22 }.Contains(dto.Power))
        {
            throw new Exception("No valid power inserted");
        }
        Vehicle vehicle = new Vehicle
        {
            Name = dto.Name,
            RegistrationPlate = dto.RegistrationNumber,
            Power = dto.Power,
            ClientId = dto.ClientId,
            IsDeleted = false,
        };
        _vehicleRepository.Post(vehicle);
        _vehicleRepository.Save();

        return ParseToModel(vehicle);
    }

    public async Task<IEnumerable<VehicleDomainModel>> GetByClient(decimal clientId)
    {
        IEnumerable<Vehicle> vehicles = await _vehicleRepository.GetByClient(clientId);
        return ParseToModel(vehicles);
    }

    private IEnumerable<VehicleDomainModel> ParseToModel(IEnumerable<Vehicle> vehicles)
    {
        return vehicles.Select(x => ParseToModel(x));
    }
}
