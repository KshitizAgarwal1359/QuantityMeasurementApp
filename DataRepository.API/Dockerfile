# UC21: Dockerfile for DataRepository.API
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY DataRepository.API/ DataRepository.API/
RUN dotnet restore DataRepository.API/DataRepository.API.csproj
RUN dotnet publish DataRepository.API/DataRepository.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENTRYPOINT ["dotnet", "DataRepository.API.dll"]
