using ChargingStation.Domain.Models;
using ChargingStation.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChargingStation.Controller;

[ApiController]
[Route("api/[controller]")]
public class CardController : ControllerBase
{
    private ICardService _cardService;

    public CardController(ICardService cardService)
    {
        _cardService = cardService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CardDomainModel>>> GetAll()
    {
        List<CardDomainModel> cards = await _cardService.GetAll();
        return Ok(cards);
    }
}
