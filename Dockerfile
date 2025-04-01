# Etapa base: imagen de ASP.NET Core (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8081
# ENV ConnectionStrings__DefaultConnection="Server=host.docker.internal,1433;Database=NetFree;User Id=sa;Password=Admin1234!;TrustServerCertificate=True;"

# Etapa de build: imagen del SDK de .NET para compilar la aplicació
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet tool install --global dotnet-ef --version 8.0.0
COPY ["netfree.sln", "./"] 
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
RUN dotnet restore
COPY . .
WORKDIR "/src/WebAPI"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet tool install --global dotnet-ef --version 8.0.0
COPY --from=publish /app/publish ./publish
COPY --from=build /src /src
COPY entrypoint.sh .
RUN chmod +x entrypoint.sh
