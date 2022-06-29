using ChargingStation.Domain.Models;
using ChargingStation.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChargingStation.Controller;

[ApiController]
[Route("api/[controller]")]
public class ManagerController : ControllerBase
{
    private IManagerService _managerService;

    public ManagerController(IManagerService managerService)
    {
        _managerService = managerService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ManagerDomainModel>>> GetAll()
    {
        List<ManagerDomainModel> managers = await _managerService.GetAll();
        return Ok(managers);
    }
}