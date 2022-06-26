using ChargingStation.Domain.DTOs;
using ChargingStation.Domain.Models;
using ChargingStation.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChargingStation.Controller;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ReservationDomainModel>>> GetAll()
    {
        List<ReservationDomainModel> reservations = await _reservationService.GetAll();
        return Ok(reservations);
    }
    public async Task<ActionResult<PlaceDomainModel>> CreateReservation(ReservationDTO dto)
    {
        ReservationDomainModel reservation = await _reservationService.CreateReservation(dto);
        return Ok(reservation);
    }
}
