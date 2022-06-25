using ChargingStation.Data.Entity;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface IBasePriceService : IService<BasePriceDomainModel>
{
}

public class BasePriceService : IBasePriceService
{
    private readonly IBasePriceRepository _basePriceRepository;

    public BasePriceService(IBasePriceRepository basePriceRepository)
    {
        _basePriceRepository = basePriceRepository;
    }
    
    public async Task<List<BasePriceDomainModel>> GetAll()
    {
        List<BasePrice> basePrices = await _basePriceRepository.GetAll();
        List<BasePriceDomainModel> result = new List<BasePriceDomainModel>();
        foreach (var item in basePrices)
            result.Add(ParseToModel(item));
        return result;
    }

    public static BasePriceDomainModel ParseToModel(BasePrice basePrice)
    {
        return new BasePriceDomainModel
        {
            Id = basePrice.Id,
            IsDeleted = basePrice.IsDeleted,
            StationId = basePrice.StationId,
            ApplyTime = basePrice.ApplyTime,
            DayAmount = basePrice.DayAmount,
            NightAmount = basePrice.NightAmount
        };
    }
}
