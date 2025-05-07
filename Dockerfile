# Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Proje dosyasını kopyala ve restore et
COPY ["MasrafTakipApi.csproj", "."]
RUN dotnet restore "MasrafTakipApi.csproj"

# Tüm dosyaları kopyala
COPY . .
RUN dotnet publish "MasrafTakipApi.csproj" -c Release -o /app/publish

# Runtime imajı
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MasrafTakipApi.dll"]