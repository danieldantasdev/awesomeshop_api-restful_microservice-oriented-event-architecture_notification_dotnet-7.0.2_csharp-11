namespace AwesomeShop.Services.Notifications.Core.Events;

public class CustomerCreatedEvent
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}