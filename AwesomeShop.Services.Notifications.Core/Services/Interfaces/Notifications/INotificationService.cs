namespace AwesomeShop.Services.Notifications.Core.Services.Interfaces.Notifications;

public interface INotificationService
{
    Task SendAsync(string subject, string content, string toEmail, string toName);
}