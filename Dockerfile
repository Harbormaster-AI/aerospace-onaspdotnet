FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY aerospaceonaspdotnet/Api.csproj aerospaceonaspdotnet/
RUN dotnet restore aerospaceonaspdotnet/Api.csproj

COPY aerospaceonaspdotnet/ aerospaceonaspdotnet/

RUN dotnet publish aerospaceonaspdotnet/Api.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Api.dll"]