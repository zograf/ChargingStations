using ChargingStation.Data.Entity;
using ChargingStation.Domain.Models;
using ChargingStation.Repository;

namespace ChargingStation.Service;

public interface INotificationService : IService<NotificationDomainModel>
{
    public Task<List<NotificationDomainModel>> GetByUserId(decimal id);
}

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IClientRepository _clientRepository;

    public NotificationService(INotificationRepository notificationRepository,
        IClientRepository clientRepository)
    {
        _notificationRepository = notificationRepository;
        _clientRepository = clientRepository;
    }

    public async Task<List<NotificationDomainModel>> GetByUserId(decimal id)
    {
        Client client = await _clientRepository.GetByUserId(id);
        List<Notification> notifications = await _notificationRepository.GetByClientId(client.Id);
        List<NotificationDomainModel> list = new List<NotificationDomainModel>();
        if (notifications.Count != 0)
            list.Add(new NotificationDomainModel());
        List<NotificationDomainModel> result = new List<NotificationDomainModel>();
        foreach (var item in notifications)
        {
            item.IsRead = true;
            _notificationRepository.Update(item);
        }
        _notificationRepository.Save();
        return list;
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
