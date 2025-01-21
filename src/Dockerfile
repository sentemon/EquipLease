FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 5242

RUN apt-get update && apt-get install -y curl && apt-get clean

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY EquipLease.Api/EquipLease.Api.csproj EquipLease.Api/
COPY EquipLease.Core/EquipLease.Core.csproj EquipLease.Core/
COPY EquipLease.Infrastructure/EquipLease.Infrastructure.csproj EquipLease.Infrastructure/

RUN dotnet restore EquipLease.Api/EquipLease.Api.csproj

COPY . .

WORKDIR /src/EquipLease.Api

RUN dotnet build EquipLease.Api.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish EquipLease.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "EquipLease.Api.dll"]