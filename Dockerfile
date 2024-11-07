# Use the .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Clear the NuGet cache to ensure clean package resolution
RUN dotnet nuget locals all --clear

# Add the NuGet source explicitly in case of connectivity issues (only if not already added)
RUN dotnet nuget list source | grep -q 'nuget.org' || dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org

# Copy the custom NuGet.Config to ensure correct package resolution paths
COPY AuthenticationService/NuGet.Config /root/.nuget/NuGet.Config

# Copy the .csproj file and restore dependencies
COPY AuthenticationService/AuthenticationService.csproj ./AuthenticationService/

# Run dotnet restore to restore the dependencies
RUN dotnet restore "AuthenticationService/AuthenticationService.csproj"

# Copy the entire source code into the container
COPY AuthenticationService/. ./AuthenticationService/

WORKDIR "/src/AuthenticationService"

# Build the application in Release mode
RUN dotnet build -c Release -o /app/build

# Publish the application in Release mode to the publish directory
RUN dotnet publish -c Release -o /app/publish

# Runtime stage: use a smaller runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /app/publish .

# Expose the necessary port (change to 8001)
EXPOSE 8001

# Set environment variable for the new port (if your app uses an environment variable to bind port)
ENV ASPNETCORE_URLS=http://+:8001

# Start the application
ENTRYPOINT ["dotnet", "AuthenticationService.dll"]
