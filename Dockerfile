# Sử dụng SDK image để build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy tất cả các file csproj trước để tối ưu cache
COPY ["PRN232.Lab2.CoffeeStore.API/PRN232.Lab2.CoffeeStore.API.csproj", "PRN232.Lab2.CoffeeStore.API/"]
COPY ["PRN232.Lab2.CoffeeStore.Services/PRN232.Lab2.CoffeeStore.Services.csproj", "PRN232.Lab2.CoffeeStore.Services/"]
COPY ["PRN232.Lab2.CoffeeStore.Repositories/PRN232.Lab2.CoffeeStore.Repositories.csproj", "PRN232.Lab2.CoffeeStore.Repositories/"]


# Restore dependencies
RUN dotnet restore "PRN232.Lab2.CoffeeStore.API/PRN232.Lab2.CoffeeStore.API.csproj"

# Copy toàn bộ source code
COPY . .

# Build
WORKDIR "/src/PRN232.Lab2.CoffeeStore.API"
RUN dotnet build "PRN232.Lab2.CoffeeStore.API.csproj" -c Release -o /app/build
#build in release mode because not containing symbol for debug ( causing high size image )

# Publish app
FROM build AS publish
RUN dotnet publish "PRN232.Lab2.CoffeeStore.API.csproj" -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose port
EXPOSE 8080

ENTRYPOINT ["dotnet", "PRN232.Lab2.CoffeeStore.API.dll"]
