using ChargingStation.Domain.Models;
using ChargingStation.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChargingStation.Controller;

[ApiController]
[Route("api/[controller]")]
public class RealtimeReportController : ControllerBase
{
    private readonly IChargingSpotService _chargingSpotService;
    private readonly ISimulationService _simulationService;

    public RealtimeReportController(IChargingSpotService chargingSpotService, ISimulationService simulationService)
    {
        _chargingSpotService = chargingSpotService;
        _simulationService = simulationService;
    }

    [HttpGet]
    public async Task<ActionResult<Dictionary<decimal, decimal>>> GetAll()
    {
        Dictionary<decimal, decimal> statesById = new Dictionary<decimal, decimal>();
        List<ChargingSpotDomainModel> spots = await _chargingSpotService.GetAll();
        foreach (var spot in spots)
            statesById[spot.Id] = spot.State;
        return Ok(statesById);
    }

    [HttpPut]
    [Route("repair")]
    public async Task<ActionResult> TriggerRepair()
    {
        await _simulationService.Repair();
        return Ok();
    }

    [HttpPut]
    [Route("malfunction")]
    public async Task<ActionResult> TriggerMalfunction()
    {
        await _simulationService.Malfunction();
        return Ok();
    }

    [HttpPut]
    [Route("arrive")]
    public async Task<ActionResult> TriggerArrive()
    {
        await _simulationService.Arrive();
        return Ok();
    }

    [HttpPut]
    [Route("reserve")]
    public async Task<ActionResult> TriggerReserve()
    {
        await _simulationService.Reserve();
        return Ok();
    }
}