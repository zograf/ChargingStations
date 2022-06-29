using ChargingStation.Domain.DTOs;
using ChargingStation.Domain.Models;
using ChargingStation.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChargingStation.Controller;

[ApiController]
[Route("api/[controller]")]
public class VehicleController : ControllerBase
{
    private IVehicleService _vehicleService;

    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet]
    public async Task<ActionResult<List<VehicleDomainModel>>> GetAll()
    {
        List<VehicleDomainModel> vehicles = await _vehicleService.GetAll();
        return Ok(vehicles);
    }

    [HttpGet]
    [Route("/{clientId}")]
    public async Task<ActionResult<List<VehicleDomainModel>>> GetByClient(decimal clientId)
    {
        IEnumerable<VehicleDomainModel> vehicles = await _vehicleService.GetByClient(clientId);
        return Ok(vehicles);
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<List<VehicleDomainModel>>> Create(VehicleDTO vehicleDTO)
    {
        VehicleDomainModel vehicle = await _vehicleService.Create(vehicleDTO);
        return Ok(vehicle);
    }
}