using ChargingStation.Data.Entity;
using ChargingStation.Domain.Models;
using ChargingStation.Domain.Utilities;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface IPriceService : IService<BasePriceDomainModel>
{
    public Task<decimal> GetCurrentPrice(decimal stationId);
}

public class PriceService : IPriceService
{
    private readonly IBasePriceRepository _basePriceRepository;

    public PriceService(IBasePriceRepository basePriceRepository)
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

    public async Task<decimal> GetCurrentPrice(decimal stationId)
    {
        BasePrice basePrice = await _basePriceRepository.GetByStation(stationId);
        decimal price;
        price = DateTimeUtils.IsDay() ? basePrice.DayAmount : basePrice.NightAmount;
        return price;
    }
}
