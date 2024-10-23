# https://hub.docker.com/r/microsoft/dotnet-sdk
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build-env

WORKDIR /NotamManagement

# Copy projects into the container. We need Guildy.Shared because Guildy.Ui depends on it.
COPY ["NotamManagement.Api/", "NotamManagement.Api/"]
COPY ["NotamManagement.Core/", "NotamManagement.Core/"]

# Restore the project and build it with output folder /build
RUN dotnet restore "NotamManagement.Api/NotamManagement.Api.csproj"
RUN dotnet build "NotamManagement.Api/NotamManagement.Api.csproj" -c Release -o /build

# Once we're done building, we'll publish the project to the /publish folder
FROM build-env AS publish
RUN dotnet publish "NotamManagement.Api/NotamManagement.Api.csproj" -c Release -o /publish


# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

EXPOSE 8080

ENV ASPNETCORE_ENVIRONMENT=Production

WORKDIR /app

COPY --from=publish /publish ./

ENTRYPOINT ["dotnet", "NotamManagement.Api.dll"]
