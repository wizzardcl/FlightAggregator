FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine

COPY /bin/Release/net8.0/publish/ app/
WORKDIR /app
ENTRYPOINT ["dotnet", "FlightsAggregator.Server.dll"]