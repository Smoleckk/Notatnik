#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Notatnik/Server/Notatnik.Server.csproj", "Notatnik/Server/"]
COPY ["Notatnik/Client/Notatnik.Client.csproj", "Notatnik/Client/"]
COPY ["Notatnik/Shared/Notatnik.Shared.csproj", "Notatnik/Shared/"]
RUN dotnet restore "Notatnik/Server/Notatnik.Server.csproj"
COPY . .
WORKDIR "/src/Notatnik/Server"
RUN dotnet build "Notatnik.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notatnik.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notatnik.Server.dll"]