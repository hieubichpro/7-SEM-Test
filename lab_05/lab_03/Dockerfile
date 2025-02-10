FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["lab_03.csproj", "./"]

RUN dotnet restore "lab_03.csproj"
COPY . .
WORKDIR "/src"

RUN dotnet build "lab_03.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "lab_03.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "lab_03.dll"]