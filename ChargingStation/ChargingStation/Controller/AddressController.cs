using ChargingStation.Domain.Models;
using ChargingStation.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChargingStation.Controller;

public class DTO
{
    public string name { get; set; }
    public int number { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpPut]
    [Route("moj/neki/route/{id}")]
    public async Task<ActionResult<List<AddressDomainModel>>> GetAll(decimal id)
    {
        List<AddressDomainModel> appointments = await _addressService.GetAll();
        return Ok(appointments);
    }
}