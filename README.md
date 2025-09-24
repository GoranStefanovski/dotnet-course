# MariaDB Docker Setup for .NET Development

This Docker Compose setup provides a local MariaDB instance for your .NET C# application development.

## Services Included

- **MariaDB 11.2**: Main database server
- **phpMyAdmin**: Web-based database administration tool (optional)

## Quick Start

1. **Start the services:**
   ```bash
   docker-compose up -d
   ```

2. **Stop the services:**
   ```bash
   docker-compose down
   ```

3. **View logs:**
   ```bash
   docker-compose logs -f mariadb
   ```

## Database Connection Details

- **Host**: `localhost`
- **Port**: `3306`
- **Database**: `dotnetapp`
- **Username**: `dotnetuser`
- **Password**: `dotnetpassword`
- **Root Password**: `rootpassword`

## .NET Connection String

Add this to your `appsettings.json` or `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=dotnetapp;Uid=dotnetuser;Pwd=dotnetpassword;"
  }
}
```

Or for Entity Framework Core:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=dotnetapp;User=dotnetuser;Password=dotnetpassword;TreatTinyAsBoolean=false;"
  }
}
```

## phpMyAdmin Access

- **URL**: http://localhost:8080
- **Username**: `dotnetuser`
- **Password**: `dotnetpassword`

## Database Initialization

Place any `.sql` initialization scripts in the `./init/` directory. These will be executed when the database container starts for the first time.

## Data Persistence

Database data is persisted in a Docker volume named `mariadb_data`. To reset the database:

```bash
docker-compose down -v
docker-compose up -d
```

## Customization

Copy `.env.example` to `.env` and modify the values as needed:

```bash
cp .env.example .env
```

Then update the docker-compose.yml to use environment variables if desired.

## Troubleshooting

- **Port 3306 already in use**: Change the port mapping in docker-compose.yml (e.g., `"3307:3306"`)
- **Permission issues**: Ensure Docker has proper permissions to create volumes
- **Connection issues**: Make sure the MariaDB container is fully started before connecting from your .NET app
