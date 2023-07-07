using AwesomeShop.Services.Notifications.Core.Services.Interfaces.Notifications;
using AwesomeShop.Services.Notifications.Infrastructure.Services.Implementations.Configurations;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AwesomeShop.Services.Notifications.Infrastructure.Services.Implementations.Notifications;

public class NotificationService : INotificationService
{
    private readonly NotificationConfiguration _notificationConfiguration;
    private readonly ISendGridClient _sendGridClient;

    public NotificationService(ISendGridClient sendGridClient, NotificationConfiguration notificationConfiguration)
    {
        _notificationConfiguration = notificationConfiguration;
        _sendGridClient = sendGridClient;
    }

    public async Task SendAsync(string subject, string content, string toEmail, string toName)
    {
        var from = new EmailAddress(_notificationConfiguration.FromEmail, _notificationConfiguration.FromName);
        var to = new EmailAddress(toEmail, toName);

        var message = new SendGridMessage
        {
            From = from,
            Subject = subject
        };

        message.AddContent(MimeType.Html, content);
        message.AddTo(to);

        message.SetClickTracking(false, false);
        message.SetOpenTracking(false);
        message.SetGoogleAnalytics(false);
        message.SetSubscriptionTracking(false);

        await _sendGridClient.SendEmailAsync(message);
    }
}