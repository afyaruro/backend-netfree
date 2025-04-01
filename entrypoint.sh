#!/bin/sh
echo "Aplicando migraciones..."
cd /src/Infrastructure
/root/.dotnet/tools/dotnet-ef migrations add MigracionDesdeDocker -s ../WebAPI
/root/.dotnet/tools/dotnet-ef database update -s ../WebAPI
echo "Migraciones aplicadas."
echo "Iniciando la aplicación..."
exec dotnet /app/publish/WebAPI.dll
