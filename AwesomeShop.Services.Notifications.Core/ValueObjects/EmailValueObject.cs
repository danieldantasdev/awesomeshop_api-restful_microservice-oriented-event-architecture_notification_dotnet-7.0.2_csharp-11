namespace AwesomeShop.Services.Notifications.Core.ValueObjects;

public class EmailValueObject
{
    public Guid Id { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public string Event { get; set; }
}