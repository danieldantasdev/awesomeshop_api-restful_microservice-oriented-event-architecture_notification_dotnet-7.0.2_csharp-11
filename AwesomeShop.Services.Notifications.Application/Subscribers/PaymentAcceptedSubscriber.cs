using System.Text;
using AwesomeShop.Services.Notifications.Core.Events;
using AwesomeShop.Services.Notifications.Core.Repositories.Interfaces.Notifications;
using AwesomeShop.Services.Notifications.Core.Services.Interfaces.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AwesomeShop.Services.Notifications.Application.Subscribers;

public class PaymentAcceptedSubscriber : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConnection _connection;
    private readonly IModel _model;
    private const string Queue = "notification-service/payment-accepted";
    private const string Exchange = "notification-service";
    private const string RoutingKey = "payment-accepted";

    public PaymentAcceptedSubscriber(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        var connectionFactory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        _connection = connectionFactory.CreateConnection("notifications-service-payment-accepted-consumer");

        _model = _connection.CreateModel();

        _model.ExchangeDeclare(Exchange, "topic", true);
        _model.QueueDeclare(Queue, false, false, false, null);
        _model.QueueBind(Queue, Exchange, Queue);

        _model.QueueBind(Queue, "payment-service", RoutingKey);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_model);

        consumer.Received += async (sender, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);
            var message = JsonConvert.DeserializeObject<PaymentAcceptedEvent>(contentString);

            Console.WriteLine($"[notification-service] Message PaymentAccepted received with Id {message.Id}");

            await SendEmail(message);

            _model.BasicAck(eventArgs.DeliveryTag, false);
        };

        _model.BasicConsume(Queue, false, consumer);

        return Task.CompletedTask;
    }

    private async Task<bool> SendEmail(PaymentAcceptedEvent payment)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var emailService = scope.ServiceProvider.GetService<INotificationService>();
            var mailRepository = scope.ServiceProvider.GetService<INotificationRepository>();

            var template = await mailRepository.GetTemplate("PaymentAccepted");

            var subject = template.Subject;
            var content = string.Format(template.Content, payment.Id);

            await emailService.SendAsync(subject, content, payment.Email, payment.FullName);

            return true;
        }
    }
}