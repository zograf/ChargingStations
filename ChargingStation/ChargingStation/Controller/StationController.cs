using ChargingStation.Domain.Models;
using ChargingStation.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChargingStation.Controller;

[ApiController]
[Route("api/[controller]")]
public class StationController : ControllerBase
{
    private IStationService _stationService;

    public StationController(IStationService stationService)
    {
        _stationService = stationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<StationDomainModel>>> GetAll()
    {
        List<StationDomainModel> stations = await _stationService.GetAll();
        return Ok(stations);
    }
}
