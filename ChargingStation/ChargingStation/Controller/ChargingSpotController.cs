using ChargingStation.Domain.Models;
using ChargingStation.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChargingStation.Controller;

[ApiController]
[Route("api/[controller]")]
public class ChargingSpotController : ControllerBase
{
    private IChargingSpotService _chargingSpotService;

    public ChargingSpotController(IChargingSpotService chargingSpotService)
    {
        _chargingSpotService = chargingSpotService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ChargingSpotDomainModel>>> GetAll()
    {
        List<ChargingSpotDomainModel> chargingSpots = await _chargingSpotService.GetAll();
        return Ok(chargingSpots);
    }
}