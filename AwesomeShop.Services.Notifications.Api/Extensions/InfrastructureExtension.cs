using AwesomeShop.Services.Notifications.Core.Repositories.Interfaces.Notifications;
using AwesomeShop.Services.Notifications.Core.Services.Interfaces.Notifications;
using AwesomeShop.Services.Notifications.Infrastructure.Persistence.Repositories.Implementations.Notifications;
using AwesomeShop.Services.Notifications.Infrastructure.Persistence.Repositories.Options;
using AwesomeShop.Services.Notifications.Infrastructure.Services.Implementations.Configurations;
using AwesomeShop.Services.Notifications.Infrastructure.Services.Implementations.Notifications;
using MongoDB.Bson;
using MongoDB.Driver;
using SendGrid.Extensions.DependencyInjection;

namespace AwesomeShop.Services.Notifications.Api.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddRepositoriesExtension(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<INotificationRepository, NotificationRepository>();
        return serviceCollection;
    }

    public static IServiceCollection AddMongoExtension(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton(sp =>
        {
            var configuration = sp.GetService<IConfiguration>();
            var options = new MongoDbOption();

            configuration?.GetSection("MongoDb").Bind(options);

            return options;
        });

        serviceCollection.AddSingleton<IMongoClient>(sp =>
        {
            var options = sp.GetService<MongoDbOption>();

            return new MongoClient(options?.ConnectionString);
        });

        serviceCollection.AddTransient(sp =>
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

            var options = sp.GetService<MongoDbOption>();
            var mongoClient = sp.GetService<IMongoClient>();

            return mongoClient?.GetDatabase(options?.Database);
        });

        return serviceCollection;
    }

    public static IServiceCollection AddServicesExtension(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var config = new NotificationConfiguration();

        configuration.GetSection("Notifications").Bind(config);
        serviceCollection.AddSingleton<NotificationConfiguration>(m => config);
        serviceCollection.AddSendGrid(sp => sp.ApiKey = config.SendGridApiKey);
        serviceCollection.AddTransient<INotificationService, NotificationService>();

        return serviceCollection;
    }
}