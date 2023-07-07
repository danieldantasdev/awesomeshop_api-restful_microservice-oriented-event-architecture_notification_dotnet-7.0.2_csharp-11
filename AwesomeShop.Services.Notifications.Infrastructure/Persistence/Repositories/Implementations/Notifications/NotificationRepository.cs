using AwesomeShop.Services.Notifications.Core.Repositories.Interfaces.Notifications;
using AwesomeShop.Services.Notifications.Core.ValueObjects;
using MongoDB.Driver;

namespace AwesomeShop.Services.Notifications.Infrastructure.Persistence.Repositories.Implementations.Notifications;

public class NotificationRepository : INotificationRepository
{
    private readonly IMongoCollection<EmailValueObject> _mongoCollection;

    public NotificationRepository(IMongoDatabase mongoDatabase)
    {
        _mongoCollection = mongoDatabase.GetCollection<EmailValueObject>("email-templates");
    }

    public async Task<EmailValueObject> GetTemplate(string @event)
    {
        return await _mongoCollection.Find(c => c.Event == @event).SingleOrDefaultAsync();
    }
}