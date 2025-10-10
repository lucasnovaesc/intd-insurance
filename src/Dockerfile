FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ContractingService/APIContracting/APIContracting.csproj", "ContractingService/APIContracting/"]
RUN dotnet restore "ContractingService/APIContracting/APIContracting.csproj"
COPY . .
WORKDIR "/src/ContractingService/APIContracting"
RUN dotnet build "APIContracting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APIContracting.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIContracting.dll"]
