# Step 1: Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy the project files to the Docker container
COPY *.csproj .
RUN dotnet restore

# Copy the remaining source code files and build the project
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Step 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the build output from the previous stage
COPY --from=build /app/publish .

# Specify the entry point for the console application
ENTRYPOINT ["dotnet", "lab_01.dll"]