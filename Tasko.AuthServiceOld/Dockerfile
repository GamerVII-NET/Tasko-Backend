#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Tasko.AuthService/Tasko.AuthService.csproj", "Tasko.AuthService/"]
COPY ["Tasko.Configuration/Tasko.Configuration.csproj", "Tasko.Configuration/"]
COPY ["Tasko.Jwt/Tasko.Jwt.csproj", "Tasko.Jwt/"]
COPY ["Tasko.Cryptography/Tasko.Cryptography.csproj", "Tasko.Cryptography/"]
COPY ["Tasko.Domains/Tasko.Domains.csproj", "Tasko.Domains/"]
COPY ["Tasko.Mongo/Tasko.Mongo.csproj", "Tasko.Mongo/"]
COPY ["Tasko.Logger/Tasko.Logger.csproj", "Tasko.Logger/"]
COPY ["Tasko.Validation/Tasko.Validation.csproj", "Tasko.Validation/"]
RUN dotnet restore "Tasko.AuthService/Tasko.AuthService.csproj"
COPY . .
WORKDIR "/src/Tasko.AuthService"
RUN dotnet build "Tasko.AuthService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tasko.AuthService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasko.AuthService.dll"]