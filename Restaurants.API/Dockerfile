# Use the ASP.NET Core runtime image as base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the .csproj files and restore dependencies first
COPY Restaurants.API/Restaurants.API.csproj Restaurants.API/
COPY Restaurants.Application/Restaurants.Application.csproj Restaurants.Application/
COPY Restaurants.Domain/Restaurants.Domain.csproj Restaurants.Domain/
COPY Restaurants.Infrastructure/Restaurants.Infrastructure.csproj Restaurants.Infrastructure/

# Restore dependencies
RUN dotnet restore "Restaurants.API/Restaurants.API.csproj"

# Now copy the rest of the application files into the container
COPY . .

# Set the working directory to the API project folder
WORKDIR "/src/Restaurants.API"

# Verify that the files are copied correctly
RUN ls -la

# Build the application with detailed logging
RUN dotnet build "Restaurants.API.csproj" -c $BUILD_CONFIGURATION -o /app/build --verbosity detailed

# Publish the application
FROM build AS publish
RUN dotnet publish "Restaurants.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Set up the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Restaurants.API.dll"]
