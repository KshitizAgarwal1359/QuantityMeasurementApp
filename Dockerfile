# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copy everything
COPY . .

# Restore based on WebApi main project (will pull in others via reference)
RUN dotnet restore QuantityMeasurement.WebApi/QuantityMeasurement.WebApi.csproj

# Publish
RUN dotnet publish QuantityMeasurement.WebApi/QuantityMeasurement.WebApi.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:${PORT:-5009}
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENV DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE=false

ENTRYPOINT ["dotnet", "QuantityMeasurement.WebApi.dll"]
