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

    [HttpGet]
    [Route("cancel/id={id}")]
    public async Task<ActionResult<ReservationDomainModel>> Cancel(decimal id)
    {
        ReservationDomainModel reservation = await _reservationService.Cancel(id);
        return Ok(reservation);
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<PlaceDomainModel>> CreateReservation(ReservationDTO dto)
    {
        ReservationDomainModel reservation = await _reservationService.CreateReservation(dto);
        return Ok(reservation);
    }

    [HttpPost]
    [Route("reserved-slots")]
    public async Task<ActionResult<List<Tuple<DateTime, DateTime>>>> GetReservedTimeSlots(decimal slotId)
    {
        IEnumerable<Tuple<DateTime, DateTime>> reservationTimeSlots = await _reservationService.GetReservedTimeSlots(slotId);
        return Ok(reservationTimeSlots);
    }

    [HttpGet]
    [Route("arrive/reservationId={id}")]
    public async Task<ActionResult<ReservationDomainModel>> Arrive(decimal id)
    {
        ReservationDomainModel reservation = await _reservationService.Arrive(id);
        return Ok(reservation);
    }
}