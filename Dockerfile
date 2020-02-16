FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /src
COPY ["worker_service.csproj", "./"]
RUN dotnet restore "./worker_service.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "worker_service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "worker_service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "worker_service.dll"]
