namespace AwesomeShop.Services.Notifications.Core.Events;

public class PaymentAcceptedEvent
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}