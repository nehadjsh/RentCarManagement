# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /RentCarManagement

# Copy and Restore 
COPY *.sln .
COPY RentCarManagement/*.csproj ./RentCarManagement/
RUN dotnet restore

# Copy everything else and build the app
COPY RentCarManagement/. ./RentCarManagement/
WORKDIR /RentCarManagement
RUN dotnet publish -c Release -o /app --no-restore

# Create the final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

# Set ASPNETCORE_ENVIRONMENT to Development for detailed error messages
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "RentCarManagement.dll"]
