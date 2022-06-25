using ChargingStation.Domain.Models;
using ChargingStation.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChargingStation.Controller;

[ApiController]
[Route("api/[controller]")]
public class CredentialsController : ControllerBase
{
    private ICredentialsService _credentialsService;

    public CredentialsController(ICredentialsService credentialsService)
    {
        _credentialsService = credentialsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CredentialsDomainModel>>> GetAll()
    {
        List<CredentialsDomainModel> credentials = await _credentialsService.GetAll();
        return Ok(credentials);
    }
}
