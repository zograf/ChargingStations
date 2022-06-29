using ChargingStation.Domain.Models;
using ChargingStation.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChargingStation.Controller;

[ApiController]
[Route("api/[controller]")]
public class PlaceController : ControllerBase
{
    private IPlaceService _placeService;

    public PlaceController(IPlaceService placeService)
    {
        _placeService = placeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PlaceDomainModel>>> GetAll()
    {
        List<PlaceDomainModel> places = await _placeService.GetAll();
        return Ok(places);
    }
}