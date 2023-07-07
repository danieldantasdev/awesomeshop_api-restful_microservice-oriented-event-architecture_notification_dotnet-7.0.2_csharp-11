using AwesomeShop.Services.Notifications.Application.Subscribers;

namespace AwesomeShop.Services.Notifications.Api.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddSubscribersExtension(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHostedService<CustomerCreatedSubscriber>();
        serviceCollection.AddHostedService<OrderCreatedSubscriber>();
        serviceCollection.AddHostedService<PaymentAcceptedSubscriber>();

        return serviceCollection;
    }
}