# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY SubsequenceFinder.sln .
COPY SubsequenceFinder/SubsequenceFinder.csproj SubsequenceFinder/
RUN dotnet restore SubsequenceFinder/SubsequenceFinder.csproj

COPY SubsequenceFinder/ SubsequenceFinder/
RUN dotnet publish SubsequenceFinder/SubsequenceFinder.csproj \
    --no-restore \
    --configuration Release \
    --output /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SubsequenceFinder.dll"]
