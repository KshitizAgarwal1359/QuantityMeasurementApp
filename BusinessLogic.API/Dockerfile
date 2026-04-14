# UC21: Dockerfile for BusinessLogic.API
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY BusinessLogic.API/ BusinessLogic.API/
RUN dotnet restore BusinessLogic.API/BusinessLogic.API.csproj
RUN dotnet publish BusinessLogic.API/BusinessLogic.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENTRYPOINT ["dotnet", "BusinessLogic.API.dll"]
