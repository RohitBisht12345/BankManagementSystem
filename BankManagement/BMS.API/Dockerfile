#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BMS.API/BMS.API.csproj", "BMS.API/"]
COPY ["BMS.Services/BMS.Services.csproj", "BMS.Services/"]
COPY ["BMS.Models/BMS.Models.csproj", "BMS.Models/"]
COPY ["BMS.Infrastructure/BMS.Infrastructure.csproj", "BMS.Infrastructure/"]
RUN dotnet restore "BMS.API/BMS.API.csproj"
COPY . .
WORKDIR "/src/BMS.API"
RUN dotnet build "BMS.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BMS.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BMS.API.dll"]
