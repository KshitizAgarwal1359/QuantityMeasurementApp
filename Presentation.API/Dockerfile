# UC21: Dockerfile for Presentation.API (API Gateway)
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY Presentation.API/ Presentation.API/
RUN dotnet restore Presentation.API/Presentation.API.csproj
RUN dotnet publish Presentation.API/Presentation.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENTRYPOINT ["dotnet", "Presentation.API.dll"]
