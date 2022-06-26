using ChargingStation.Domain.Models;
using ChargingStation.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChargingStation.Controller;

[ApiController]
[Route("api/[controller]")]
public class BasePriceController : ControllerBase
{
    private IPriceService _basePriceService;

    public BasePriceController(IPriceService basePriceService)
    {
        _basePriceService = basePriceService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BasePriceDomainModel>>> GetAll()
    {
        List<BasePriceDomainModel> basePrices = await _basePriceService.GetAll();
        return Ok(basePrices);
    }
}
