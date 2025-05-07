# MasrafTakipApi

**MasrafTakipApi**, .NET 9 ve ASP.NET Core kullanılarak geliştirilmiş bir harcama takip (expense tracking) Web API projesidir. Entity Framework Core ile PostgreSQL veritabanı yönetimi, kod–ilk (code‑first) migrations ve RabbitMQ mesaj kuyruğu desteği sunar. Katmanlı mimarisi (Controller → Service → Repository) ve Docker Compose ile hem API, veritabanı hem de RabbitMQ servislerini konteyner olarak tek komutla ayağa kaldırabilirsiniz.

---

## İçindekiler

* [Proje Hakkında](#proje-hakkında)
* [Öne Çıkan Özellikler](#öne-çıkan-özellikler)
* [Kullanılan Teknolojiler](#kullanılan-teknolojiler)
* [Önkoşullar](#önkoşullar)
* [Kurulum ve Çalıştırma](#kurulum-ve-çalıştırma)
* [Konfigürasyon ve Ortam Değişkenleri](#konfigürasyon-ve-ortam-değişkenleri)
* [Veritabanı ve Migrations](#veritabanı-ve-migrations)
* [Docker ile Çalıştırma](#docker-ile-çalıştırma)
* [RabbitMQ Mesaj Kuyruğu](#rabbitmq-mesaj-kuyruğu)
* [API Dokümantasyonu (Swagger)](#api-dokümantasyonu-swagger)
* [CI/CD (GitHub Actions)](#cicd-github-actions)
* [Proje Mimari ve Klasör Yapısı](#proje-mimari-ve-klasör-yapısı)
* [Katkıda Bulunma](#katkıda-bulunma)
* [Kod Davranış Kuralları ve Code of Conduct](#kod-davranış-kuralları-ve-code-of-conduct)
* [Lisans](#lisans)
* [İletişim](#iletişim)

---

## Proje Hakkında

**MasrafTakipApi**, kuruluşların ve bireylerin harcama kayıtlarını takip edebilmeleri için tasarlanmış, .NET 9 ve ASP.NET Core ile geliştirilmiş bir Web API projesidir. Veritabanı işlemleri için Entity Framework Core, mesaj kuyruğu işlemleri için RabbitMQ, veri dönüşümleri için AutoMapper ve doğrulamalar için FluentValidation kullanır. Docker Compose ile API, PostgreSQL ve RabbitMQ servislerini tek adımda ayağa kaldırabilirsiniz.

---

## Öne Çıkan Özellikler

* **CRUD Operasyonları**: Harcama (`Expense`) varlıkları için listeleme, getirme, oluşturma, güncelleme ve silme.
* **Katmanlı Mimari**: Controller, Service ve Repository katmanlarıyla temiz kod ve test edilebilirlik.
* **Entity Framework Core Migrations**: Kod–ilk (Code‑First) yaklaşımı.
* **RabbitMQ Entegrasyonu**: Asenkron mesaj kuyruğu işlemleri ve hafif kuyruk bazlı iş akışları.
* **AutoMapper**: DTO ↔ Entity dönüşümleri.
* **FluentValidation**: Özelleştirilebilir doğrulama kuralları.
* **Swagger/OpenAPI**: Otomatik API dokümantasyonu.
* **HTTP Floating File**: `MasrafTakipApi.http` ile örnek istekler.
* **Docker Compose**: API, PostgreSQL ve RabbitMQ servisleri.

---

## Kullanılan Teknolojiler

* .NET 9 / ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* RabbitMQ (v3.9+)
* Docker & Docker Compose
* AutoMapper
* FluentValidation
* Swashbuckle (Swagger/OpenAPI)

---

## Önkoşullar

1. .NET 9 SDK ([https://dotnet.microsoft.com/download/dotnet/9.0](https://dotnet.microsoft.com/download/dotnet/9.0))
2. Docker & Docker Compose (17.06+ ve Compose V2+)
3. RabbitMQ Management Plugin ile birlikte RabbitMQ sunucusu ([http://localhost:15672](http://localhost:15672))
4. Tercihe bağlı olarak pgAdmin 4 veya başka bir PostgreSQL istemcisi.

---

## Kurulum ve Çalıştırma

```bash
# Depoyu klonlayın
git clone https://github.com/aysavsar/MasrafTakipApi.git
cd MasrafTakipApi

# NuGet paketlerini restore ve proje derlemesi
dotnet restore
dotnet build

# Uygulamayı Development profilinde çalıştırın
dotnet run --launch-profile "Development"
```

* Uygulama `https://localhost:5001` adresinde çalışır.
* Swagger UI: `https://localhost:5001/swagger/index.html`

---

## Konfigürasyon ve Ortam Değişkenleri

`appsettings.json` ve `appsettings.Development.json` dosyalarında:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=MasrafDb;Username=postgres;Password=secret"
  },
  "RabbitMQ": {
    "HostName": "localhost",
    "Port": 5672,
    "UserName": "guest",
    "Password": "guest",
    "VirtualHost": "/"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  }
}
```

**Not:** Hassas bilgileri (şifreler, kullanıcı adları) GitHub Secrets veya ortam değişkenleri ile yönetin.

---

## Veritabanı ve Migrations

```bash
# İlk migration'ı oluşturun
dotnet ef migrations add InitialCreate

# Veritabanını güncelleyin
dotnet ef database update
```

Her model değişikliğinde migrations adımlarını tekrarlayarak veritabanı şemasını güncel tutun.

---

## Docker ile Çalıştırma

```bash
docker-compose up -d
```

* API: `https://localhost:5001`
* PostgreSQL: `localhost:5432`
* RabbitMQ Management: `http://localhost:15672` (guest/guest)

`docker-compose.yml` örneği:

```yaml
version: '3.8'
services:
  api:
    build: .
    ports:
      - "5001:5001"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
  db:
    image: postgres:15
    environment:
      POSTGRES_DB: MasrafDb
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: secret
    volumes:
      - pgdata:/var/lib/postgresql/data
  rabbitmq:
    image: rabbitmq:3.9-management
    ports:
      - "5672:5672"
      - "15672:15672"
    environment:
      RABBITMQ_DEFAULT_USER: guest
      RABBITMQ_DEFAULT_PASS: guest
volumes:
  pgdata:
```

---

## RabbitMQ Mesaj Kuyruğu

* Management UI erişimi: [http://localhost:15672](http://localhost:15672)
* Kuyruk adı: `expense-queue`
* Mesaj gönderme ve alma işlemleri `IRabbitMQService` üzerinden soyutlanmıştır.
* `RabbitMQPublisher` ve `RabbitMQConsumer` sınıflarıyla async mesajlaşma sağlanır.

---

## API Dokümantasyonu (Swagger)

Proje çalıştığında `https://localhost:5001/swagger/index.html` adresinde interaktif dokümantasyon sunar.

Örnek:

```csharp
[HttpPost]
public async Task<ActionResult<ExpenseDto>> Create([FromBody] CreateExpenseDto dto)
```

---


## CI/CD (GitHub Actions)

`.github/workflows/dotnet.yml` ile otomatik derleme ve test:

```yaml
name: .NET CI

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '9.0.x'
      - name: Restore
        run: dotnet restore
      - name: Build
        run: dotnet build --no-restore --configuration Release
      - name: Test
        run: dotnet test --no-build --verbosity normal
```

---

## Proje Mimari ve Klasör Yapısı

```
/Controllers    → HTTP uç noktaları  
/Services       → İş mantığı          
/Repositories   → Veri erişimi        
/Entities       → EF Core modelleri    
/DTOs           → Veri transfer nesneleri  
/Data           → DbContext & Migrations  
/Helpers        → Mapper profilleri vb.    
/Interfaces     → Katman sözleşmeleri     
/Migrations     → EF Core migration dosyaları
```

---

## Katkıda Bulunma

1. Repo’yu fork’layın
2. Yeni branch oluşturun (`feature/özellik-adi`)
3. Değişiklikleri commit edin
4. Branch’i push’layın
5. Pull request açın

Lütfen `.editorconfig` ve kod stiline uyunuz.

---

## Kod Davranış Kuralları ve Code of Conduct

Topluluk kurallarımızı `CODE_OF_CONDUCT.md` dosyasında bulabilirsiniz. Saygılı ve kapsayıcı olun.

---

## Lisans

Bu proje [MIT Lisansı](https://opensource.org/licenses/MIT) ile lisanslanmıştır.

---

## İletişim

Sorular veya sorun bildirimleri için [Issues](https://github.com/aysavsar/MasrafTakipApi/issues) sayfasını kullanın.
