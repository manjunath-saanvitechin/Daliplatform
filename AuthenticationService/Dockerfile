# Use the SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY ["AuthenticationService/AuthenticationService.csproj", "AuthenticationService/"]
RUN dotnet restore "AuthenticationService/AuthenticationService.csproj"

# Copy the entire source code into the container
COPY AuthenticationService/. AuthenticationService/
WORKDIR "/src/AuthenticationService"

# Build and publish the app
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose the necessary port
EXPOSE 8080

# Start the application
ENTRYPOINT ["dotnet", "AuthenticationService.dll"]
