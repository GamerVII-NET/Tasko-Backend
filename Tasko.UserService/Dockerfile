#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Tasko.UserService/Tasko.UserService.csproj", "Tasko.UserService/"]
COPY ["Tasko.Domains/Tasko.Domains.csproj", "../Tasko-Backend/Tasko.Domains/"]
COPY ["Tasko.General/Tasko.General.csproj", "../Tasko-Backend/Tasko.General/"]
RUN dotnet restore "Tasko.UserService/Tasko.UserService.csproj"
COPY . .
WORKDIR "/src/Tasko.UserService"
RUN dotnet build "Tasko.UserService.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Tasko.UserService.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasko.UserService.dll"]