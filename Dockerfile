# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/EncurtadorLink.Robot.Service/EncurtadorLink.Robot.Service.csproj", "src/Services/EncurtadorLink.Robot.Service/"]
COPY ["src/EncurtadorLink.Shared.Library/EncurtadorLink.Shared.Library.csproj", "src/EncurtadorLink.Shared.Library/"]
COPY ["src/EncurtadorLink.Entities.Library/EncurtadorLink.Entities.Library.csproj", "src/EncurtadorLink.Entities.Library/"]
COPY ["src/EncurtadorLink.UseCases.Library/EncurtadorLink.UseCases.Library.csproj", "src/EncurtadorLink.UseCases.Library/"]
COPY ["src/EncurtadorLink.Database.Library/EncurtadorLink.Database.Library.csproj", "src/EncurtadorLink.Database.Library/"]
RUN dotnet restore "./src/Services/EncurtadorLink.Robot.Service/EncurtadorLink.Robot.Service.csproj"
COPY . .
WORKDIR "/src/src/Services/EncurtadorLink.Robot.Service"
RUN dotnet build "./EncurtadorLink.Robot.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EncurtadorLink.Robot.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EncurtadorLink.Robot.Service.dll"]