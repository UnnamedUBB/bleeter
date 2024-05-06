FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Bleeter.AccountService/Bleeter.AccountService.csproj", "Bleeter.AccountService/"]
COPY ["Bleeter.AccountService.DAL/Bleeter.AccountService.DAL.csproj", "Bleeter.AccountService.DAL/"]
RUN dotnet restore "Bleeter.AccountService/Bleeter.AccountService.csproj"
COPY . .
WORKDIR "/src/Bleeter.AccountService"
RUN dotnet build "Bleeter.AccountService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Bleeter.AccountService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bleeter.AccountService.dll"]
