# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copy all projects
COPY ["Api/Api.csproj", "Api/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["Domains/Domains.csproj", "Domains/"]
COPY ["Repositories/Repositories.csproj", "Repositories/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Dtos/Dtos.csproj", "Dtos/"]

# Restore
RUN dotnet restore "Api/Api.csproj"

# Copy everything else
COPY . .
WORKDIR "/src/Api"

# Build and Publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
COPY --from=build /app/publish .

# Environment variables for security
ENV DOTNET_EnableDiagnostics=0
USER $APP_UID

ENTRYPOINT ["dotnet", "Api.dll"]
