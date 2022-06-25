using ChargingStation.Data.Entity;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface IPlaceService : IService<PlaceDomainModel>
{
}

public class PlaceService : IPlaceService
{
    private readonly IPlaceRepository _placeRepository;

    public PlaceService(IPlaceRepository placeRepository)
    {
        _placeRepository = placeRepository;
    }
    
    public async Task<List<PlaceDomainModel>> GetAll()
    {
        List<Place> places = await _placeRepository.GetAll();
        List<PlaceDomainModel> result = new List<PlaceDomainModel>();
        foreach (var item in places)
            result.Add(ParseToModel(item));
        return result;
    }

    public static PlaceDomainModel ParseToModel(Place place)
    {
        PlaceDomainModel placeModel = new PlaceDomainModel
        {
            Id = place.Id,
            IsDeleted = place.IsDeleted,
            Name = place.Name
        };

        placeModel.Addresses = new List<AddressDomainModel>();
        if (place.Addresses != null)
            foreach (var item in place.Addresses)
                placeModel.Addresses.Add(AddressService.ParseToModel(item));
        
        return placeModel;
    }
}
