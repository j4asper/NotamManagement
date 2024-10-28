![UI Uptime Badge](https://kuma.uptime-vps.jazper.dk/api/badge/7/uptime/48?labelPrefix=UI+&style=for-the-badge)
![API Uptime Badge](https://kuma.uptime-vps.jazper.dk/api/badge/8/uptime/48?labelPrefix=API+&style=for-the-badge)

# NotamManagement
Notam Management Project in collaboration with [AIRSUPPORT A/S](https://ppsflightplanning.com/).

## Docker

### Images

| Image                                     | Tags     |
|-------------------------------------------|----------|
| `registry.jazper.dk/notam-management-api` | `latest` |
| `registry.jazper.dk/notam-management-ui`  | `latest` |

### Ports

| Image                                     | Description | Internal Port |
|-------------------------------------------|-------------|---------------|
| `registry.jazper.dk/notam-management-api` | HTTP        | `8080`        |
| `registry.jazper.dk/notam-management-ui`  | HTTP        | `80`          |

### Variables

| Image                                     | Description                                                                                                          | Variable                      |
|-------------------------------------------|----------------------------------------------------------------------------------------------------------------------|-------------------------------|
| `registry.jazper.dk/notam-management-api` | PostgreSQL Database Connection String, optional if set in [appsettings.json](./NotamManagement.Api/appsettings.json) | `Database__ConnectionString`  |
