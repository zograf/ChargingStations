using ChargingStation.Domain.DTOs;
using ChargingStation.Domain.Models;
using ChargingStation.Service;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

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

    [HttpPost]
    [Route("prepaid")]
    public async Task<ActionResult<List<ClientDomainModel>>> Prepaid(TransactionDTO dto)
    {
        TransactionDomainModel transaction = await _clientService.Prepaid(dto);
        return Ok(transaction);
    }
}
