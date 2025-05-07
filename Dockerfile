# Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Tüm proje dosyalarını kopyala
COPY ["MasrafTakipApi/MasrafTakipApi.csproj", "MasrafTakipApi/"]
RUN dotnet restore "MasrafTakipApi/MasrafTakipApi.csproj"

COPY . .
WORKDIR "/src/MasrafTakipApi"

# Yayınlama yap
RUN dotnet publish "MasrafTakipApi.csproj" -c Release -o /app/publish

# Runtime imajı
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "MasrafTakipApi.dll"]