FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Bleeter.PrivateMessagesService/Bleeter.PrivateMessagesService.csproj", "Bleeter.PrivateMessagesService/"]
COPY ["Bleeter.Shared/Bleeter.Shared.csproj", "Bleeter.Shared/"]
RUN dotnet restore "Bleeter.PrivateMessagesService/Bleeter.PrivateMessagesService.csproj"
COPY . .
WORKDIR "/src/Bleeter.PrivateMessagesService"
RUN dotnet build "Bleeter.PrivateMessagesService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Bleeter.PrivateMessagesService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bleeter.PrivateMessagesService.dll"]
