# AwesomeShop.Services.Notifications - Formação Microsserviços com ASP.NET Core

A arquitetura do AwesomeShop, sistema de e-commerce baseado na arquitetura de microsserviços, contém 6 microsserviços:
- [Customers](https://github.com/danieldantasdev/awesomeshop_microservice_customers_dotnet-7.0.2)
- [Products](https://github.com/danieldantasdev/awesomeshop_microservice_products_dotnet-7.0.2)
- [Orders](https://github.com/danieldantasdev/awesomeshop_microservice_orders_dotnet-7.0.2)
- [Payments](https://github.com/danieldantasdev/awesomeshop_microservice_payments_dotnet-7.0.2)
- [Notifications](https://github.com/danieldantasdev/awesomeshop_microservice_notifications_dotnet-7.0.2)
- [API Gateway](https://github.com/danieldantasdev/awesomeshop_microservice_apigateway_dotnet-7.0.2)

## Tecnologias, práticas e arquitetura utilizadas
- ASP.NET Core com .NET 5
- Arquitetura de Microsserviços
- Princípios do Domain-Driven Design
- MongoDB
- Clean Architecture
- CQRS
- RabbitMQ e arquitetura orientada a eventos
- API Gateway com Ocelot
- Service Discovery com Consul
- Observabilidade com Prometheus e Grafana

## Funcionalidades do Notifications
- Envio de e-mail baseado nos eventos de domínio:
    - PaymentAccepted
    - OrderCreated
    - CustomerCreated
