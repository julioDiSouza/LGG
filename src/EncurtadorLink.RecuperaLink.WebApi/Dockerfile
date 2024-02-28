#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/EncurtadorLink.RecuperaLink.WebApi/EncurtadorLink.RecuperaLink.WebApi.csproj", "src/EncurtadorLink.RecuperaLink.WebApi/"]
COPY ["src/EncurtadorLink.UseCases.Library/EncurtadorLink.UseCases.Library.csproj", "src/EncurtadorLink.UseCases.Library/"]
COPY ["src/EncurtadorLink.Database.Library/EncurtadorLink.Database.Library.csproj", "src/EncurtadorLink.Database.Library/"]
COPY ["src/EncurtadorLink.Entities.Library/EncurtadorLink.Entities.Library.csproj", "src/EncurtadorLink.Entities.Library/"]
COPY ["src/EncurtadorLink.Shared.Library/EncurtadorLink.Shared.Library.csproj", "src/EncurtadorLink.Shared.Library/"]
RUN dotnet restore "./src/EncurtadorLink.RecuperaLink.WebApi/./EncurtadorLink.RecuperaLink.WebApi.csproj"
COPY . .
WORKDIR "/src/src/EncurtadorLink.RecuperaLink.WebApi"
RUN dotnet build "./EncurtadorLink.RecuperaLink.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EncurtadorLink.RecuperaLink.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EncurtadorLink.RecuperaLink.WebApi.dll"]