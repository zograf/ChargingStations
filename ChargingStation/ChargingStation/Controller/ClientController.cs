using ChargingStation.Domain.Models;
using ChargingStation.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChargingStation.Controller;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ClientDomainModel>>> GetAll()
    {
        List<ClientDomainModel> clients = await _clientService.GetAll();
        return Ok(clients);
    }

    [HttpGet]
    [Route("userId={id}")]
    public async Task<ActionResult<ClientDomainModel>> GetByUserId(decimal id)
    {
        ClientDomainModel client = await _clientService.GetByUserId(id);
        return Ok(client);
    }
}
