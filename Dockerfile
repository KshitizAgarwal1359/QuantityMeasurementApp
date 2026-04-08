# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files first (layer caching)
COPY QuantityMeasurementApp.slnx .
COPY QuantityMeasurement/QuantityMeasurement.csproj QuantityMeasurement/
COPY QuantityMeasurement.Models/QuantityMeasurement.Models.csproj QuantityMeasurement.Models/
COPY QuantityMeasurement.Repository/QuantityMeasurement.Repository.csproj QuantityMeasurement.Repository/
COPY QuantityMeasurement.Service/QuantityMeasurement.Service.csproj QuantityMeasurement.Service/
COPY QuantityMeasurement.Controllers/QuantityMeasurement.Controllers.csproj QuantityMeasurement.Controllers/
COPY QuantityMeasurement.WebApi/QuantityMeasurement.WebApi.csproj QuantityMeasurement.WebApi/
COPY QuantityMeasurement.Tests/QuantityMeasurement.Tests.csproj QuantityMeasurement.Tests/

# Restore
RUN dotnet restore QuantityMeasurementApp.slnx

# Copy everything and publish
COPY . .
RUN dotnet publish QuantityMeasurement.WebApi/QuantityMeasurement.WebApi.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:${PORT:-5009}
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENV DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE=false

ENTRYPOINT ["dotnet", "QuantityMeasurement.WebApi.dll"]
