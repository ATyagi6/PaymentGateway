#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PaymentGatewayDemo.API/PaymentGatewayDemo.API.csproj", "PaymentGatewayDemo.API/"]
RUN dotnet restore "PaymentGatewayDemo.API/PaymentGatewayDemo.API.csproj"
COPY . .
WORKDIR "/src/PaymentGatewayDemo.API"
RUN dotnet build "PaymentGatewayDemo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentGatewayDemo.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentGatewayDemo.API.dll"]