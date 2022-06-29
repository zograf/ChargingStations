using ChargingStation.Data.Entity;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface INotificationService : IService<NotificationDomainModel>
{
}

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationService(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }
    
    public async Task<List<NotificationDomainModel>> GetAll()
    {
        List<Notification> notifications = await _notificationRepository.GetAll();
        List<NotificationDomainModel> result = new List<NotificationDomainModel>();
        foreach (var item in notifications)
            result.Add(ParseToModel(item));
        return result;
    }

    public static NotificationDomainModel ParseToModel(Notification notification)
    {
        NotificationDomainModel notificationModel = new NotificationDomainModel
        {
            Id = notification.Id,
            IsRead = notification.IsRead,
            ClientId = notification.ClientId
        };

        return notificationModel;
    }
}
