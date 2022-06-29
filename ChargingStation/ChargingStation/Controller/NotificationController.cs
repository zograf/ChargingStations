using ChargingStation.Domain.Models;
using ChargingStation.Repository;
using ChargingStation.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChargingStation.Controller;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<NotificationDomainModel>>> GetAll()
    {
        List<NotificationDomainModel> notifications = await _notificationService.GetAll();
        return Ok(notifications);
    }

    [HttpGet]
    [Route("userId={id}")]
    public async Task<ActionResult<List<NotificationDomainModel>>> GetByUserId(decimal id)
    {
        List<NotificationDomainModel> notifications = await _notificationService.GetByUserId(id);
        return Ok(notifications);
    }
}
