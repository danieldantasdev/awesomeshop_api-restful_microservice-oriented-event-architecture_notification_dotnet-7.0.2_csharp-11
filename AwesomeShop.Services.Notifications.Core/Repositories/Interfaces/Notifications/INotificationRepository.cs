using AwesomeShop.Services.Notifications.Core.ValueObjects;

namespace AwesomeShop.Services.Notifications.Core.Repositories.Interfaces.Notifications;

public interface INotificationRepository
{
    Task<EmailValueObject> GetTemplate(string @event);
}