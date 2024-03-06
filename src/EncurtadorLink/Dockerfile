#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/EncurtadorLink/EncurtadorLink.csproj", "src/EncurtadorLink/"]
COPY ["src/EncurtadorLink.Database.Library/EncurtadorLink.Database.Library.csproj", "src/EncurtadorLink.Database.Library/"]
COPY ["src/EncurtadorLink.Entities.Library/EncurtadorLink.Entities.Library.csproj", "src/EncurtadorLink.Entities.Library/"]
COPY ["src/EncurtadorLink.Shared.Library/EncurtadorLink.Shared.Library.csproj", "src/EncurtadorLink.Shared.Library/"]
COPY ["src/EncurtadorLink.UseCases.Library/EncurtadorLink.UseCases.Library.csproj", "src/EncurtadorLink.UseCases.Library/"]
RUN dotnet restore "./src/EncurtadorLink/./EncurtadorLink.csproj"
COPY . .
WORKDIR "/src/src/EncurtadorLink"
RUN dotnet build "./EncurtadorLink.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EncurtadorLink.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EncurtadorLink.dll"]