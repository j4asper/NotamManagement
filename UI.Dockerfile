# https://hub.docker.com/r/microsoft/dotnet-sdk
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build-env

WORKDIR /NotamManagement

# Copy projects into the container. We need Guildy.Shared because Guildy.Ui depends on it.
COPY ["NotamManagement.UI/", "NotamManagement.UI/"]
COPY ["NotamManagement.Core/", "NotamManagement.Core/"]

# Restore the project and build it with output folder /build
RUN dotnet restore "NotamManagement.UI/NotamManagement.UI.csproj"
RUN dotnet build "NotamManagement.UI/NotamManagement.UI.csproj" -c Release -o /build

# Once we're done building, we'll publish the project to the /publish folder
FROM build-env AS publish
RUN dotnet publish "NotamManagement.UI/NotamManagement.UI.csproj" -c Release -o /publish

# We then get the base image for Nginx and set the work directory
FROM nginx:stable-alpine AS final

WORKDIR /usr/share/nginx/html

# We'll copy all the contents from wwwroot in the publish
# folder into nginx/html for nginx to serve. The destination
# Then we give nginx our custom config file.
COPY --from=publish /publish/wwwroot /usr/local/webapp/nginx/html
COPY ./nginx.conf /etc/nginx/conf.d/default.conf

HEALTHCHECK --interval=30s --timeout=10s --retries=3 CMD curl -f http://localhost/ || exit