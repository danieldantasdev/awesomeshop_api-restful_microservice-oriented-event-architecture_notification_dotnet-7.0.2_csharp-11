FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AwesomeShop.Services.Notifications.Api/AwesomeShop.Services.Notifications.Api.csproj", "AwesomeShop.Services.Notifications.Api/"]
RUN dotnet restore "AwesomeShop.Services.Notifications.Api/AwesomeShop.Services.Notifications.Api.csproj"
COPY . .
WORKDIR "/src/AwesomeShop.Services.Notifications.Api"
RUN dotnet build "AwesomeShop.Services.Notifications.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AwesomeShop.Services.Notifications.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AwesomeShop.Services.Notifications.Api.dll"]
