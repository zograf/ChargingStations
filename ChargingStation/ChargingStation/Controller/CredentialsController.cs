using ChargingStation.Domain.DTOs;
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
    
    [HttpPut]
    [Route("login")]
    public async Task<ActionResult<CredentialsDomainModel>> GetUser(UsernamePasswordDTO dto)
    {
        try
        {
            UserDomainModel user = await _credentialsService.GetUser(dto);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
