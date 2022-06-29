using ChargingStation.Data.Entity;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface IAddressService : IService<AddressDomainModel>
{
}

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;

    public AddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<List<AddressDomainModel>> GetAll()
    {
        List<Address> addresses = await _addressRepository.GetAll();
        List<AddressDomainModel> result = new List<AddressDomainModel>();
        foreach (var item in addresses)
            result.Add(ParseToModel(item));
        return result;
    }

    public static AddressDomainModel ParseToModel(Address address)
    {
        return new AddressDomainModel
        {
            Id = address.Id,
            Entrance = address.Entrance,
            IsDeleted = address.IsDeleted,
            Number = address.Number,
            PlaceId = address.PlaceId,
            StationId = address.StationId
        };
    }
}