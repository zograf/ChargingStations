using ChargingStation.Domain.Models;
using ChargingStation.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChargingStation.Controller;

[ApiController]
[Route("api/[controller]")]
public class ChargingController : ControllerBase
{
    private IChargingService _chargingService;

    public ChargingController(IChargingService chargingService)
    {
        _chargingService = chargingService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ChargingDomainModel>>> GetAll()
    {
        List<ChargingDomainModel> chargings = await _chargingService.GetAll();
        return Ok(chargings);
    }
}
