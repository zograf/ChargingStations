using ChargingStation.Domain.Models;
using ChargingStation.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChargingStation.Controller;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AddressDomainModel>>> GetAll()
    {
        List<AddressDomainModel> appointments = await _addressService.GetAll();
        return Ok(appointments);
    }
}