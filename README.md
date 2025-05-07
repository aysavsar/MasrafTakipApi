
# MasrafTakipApi

MasrafTakipApi, .NET 9 ve ASP.NET Core tabanlı, katmanlı mimarisi sayesinde sürdürülebilir, test edilebilir ve genişletilebilir bir harcama takip Web API’sidir. Entity Framework Core’un Code‑First migrations özelliği ile model değişiklikleri veritabanı şemasına otomatik yansıtılır. Asenkron işlemler için RabbitMQ mesaj kuyruğu kullanılır; yönetim arayüzü [http://localhost:15672](http://localhost:15672) adresinden erişilebilir. Docker Compose ile API, PostgreSQL ve RabbitMQ servisleri tek komutla ayağa kaldırılabilir. Swagger/OpenAPI (Swashbuckle) entegre edilerek interaktif dokümantasyon ve test arayüzü sunulur. Proje, GitHub Actions CI/CD, kapsamlı logging, versiyonlama, güvenlik ve katkı rehberi bölümleriyle tam bir “rapor” niteliğindedir.

## İçindekiler

1. [Proje Hakkında](#proje-hakkında)
2. [Öne Çıkan Özellikler](#öne-çıkan-özellikler)
3. [Kullanılan Teknolojiler](#kullanılan-teknolojiler)
4. [Önkoşullar](#önkoşullar)
5. [Kurulum ve Çalıştırma](#kurulum-ve-çalıştırma)
6. [Konfigürasyon & Ortam Değişkenleri](#konfigürasyon--ortam-değişkenleri)
7. [Veritabanı & Migrations](#veritabanı--migrations)
8. [RabbitMQ Mesaj Kuyruğu](#rabbitmq-mesaj-kuyruğu)
9. [Docker Compose ile Orkestrasyon](#docker-compose-ile-orkestrasyon)
10. [API Dokümantasyonu (Swagger)](#api-dokümantasyonu-swagger)
11. [Versiyonlama](#versiyonlama)
12. [Güvenlik En İyi Uygulamaları](#güvenlik-en-iyi-uygulamaları)
13. [Logging ve Monitoring](#logging-ve-monitoring)
14. [Testler](#testler)
15. [CI/CD (GitHub Actions)](#cicd-github-actions)
16. [Katkıda Bulunma & Code of Conduct](#katkıda-bulunma--code-of-conduct)
17. [Lisans](#lisans)
18. [İletişim](#iletişim)

---

## Proje Hakkında
MasrafTakipApi, kuruluşların ve bireylerin harcamalarını merkezi bir şekilde takip edip yönetmelerini sağlar. Controller → Service → Repository katmanlı mimarisiyle sorumluluklar ayrılmış, temiz kod ve test edilebilirlik garanti edilir. Entity Framework Core kod‑ilk (Code‑First) migrasyonları, model ve veritabanı şemasını senkron tutar. RabbitMQ üzerinden asenkron kuyruk işlemleri gerçekleştirilir ve `expense-queue` kuyruk ismi kullanılır. Docker Compose tanımı sayesinde tüm bileşenler tek komutla ayağa kalkar.

---

## Öne Çıkan Özellikler
- CRUD Operasyonları: `/api/expenses` uç noktalarıyla GET, POST, PUT, DELETE işlemleri yapılabilir.
- Katmanlı Mimari: Controller, Service ve Repository ayrımıyla kod bakımı ve testi kolaylaşır.
- EF Core Migrations: Kod–ilk yaklaşımıyla veritabanı şemasını güncel tutar.
- RabbitMQ Entegrasyonu: AMQP 0‑9‑1 ile güvenilir mesajlaşma sağlar.
- Swagger/OpenAPI: Swashbuckle ile otomatik dokümantasyon.
- Docker Compose: Tek adımlı ortam kurulumunu destekler.
- AutoMapper: DTO ve Entity dönüşümlerini merkezi olarak yönetir.
- FluentValidation: İstek doğrulamalarını güçlü koşullarla tanımlama imkânı sunar.
- Logging: Microsoft.Extensions.Logging altyapısıyla kapsamlı loglama.
- Versiyonlama: API versioning stratejileriyle geriye dönük uyumluluk sağlanır.
- CI/CD: GitHub Actions ile otomatik build, test ve deploy iş akışları.

---

## Kullanılan Teknolojiler
- .NET 9 / ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- RabbitMQ v3.9+
- Docker & Docker Compose
- Swashbuckle (Swagger/OpenAPI)
- AutoMapper
- FluentValidation
- GitHub Actions
- Microsoft.Extensions.Logging

---

## Önkoşullar
1. .NET 9 SDK (`dotnet --version >= 9.0`)
2. Docker Engine & Compose V2+
3. RabbitMQ (Management Plugin aktif, [http://localhost:15672](http://localhost:15672))
4. Tercihe bağlı: pgAdmin 4 veya başka bir SQL istemcisi.

---

## Kurulum ve Çalıştırma
```bash
git clone https://github.com/aysavsar/MasrafTakipApi.git
cd MasrafTakipApi
dotnet restore
dotnet build
dotnet run --launch-profile "Development"
```

Uygulama [https://localhost:5001](https://localhost:5001) adresinde hizmet verir.  
**Swagger UI**: [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html)

---

## Konfigürasyon & Ortam Değişkenleri
`appsettings.json` içeriği:
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
    "LogLevel": { "Default": "Information", "Microsoft": "Warning" }
  }
}
```
Hassas verileri GitHub Secrets veya ortam değişkenleriyle yönetin.

---

## Veritabanı & Migrations
Model değişikliklerinin veritabanına yansıması için:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## RabbitMQ Mesaj Kuyruğu
- **Management UI**: [http://localhost:15672](http://localhost:15672) (`guest`/`guest`)
- **Kuyruk Adı**: `expense-queue`
- `IRabbitMQService` ile publish/consume işlemleri soyutlanmıştır.
- `RabbitMQPublisher` ve `RabbitMQConsumer` sınıfları asenkron iş akışları sağlar.

---

## Docker Compose ile Orkestrasyon
```bash
docker-compose up -d
```

`docker-compose.yml` içeriği:
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

**Erişim Bilgileri**:
- **API**: [https://localhost:5001](https://localhost:5001)
- **DB**: `localhost:5432`
- **RabbitMQ UI**: [http://localhost:15672](http://localhost:15672)

---

## API Dokümantasyonu (Swagger)
- `builder.Services.AddSwaggerGen()` ile Swashbuckle entegrasyonu sağlanır.
- `app.UseSwagger()` ve `app.UseSwaggerUI()` çağrıları ile `/swagger/index.html` üzerinden tüm endpoint’ler keşfedilip test edilebilir.

---

## Versiyonlama
API versioning kütüphanesi ile URL veya header tabanlı versiyonlama stratejileri uygulanır. `.AddApiVersioning()` konfigürasyonu eklenir.

---

## Güvenlik En İyi Uygulamaları
- **HTTPS Zorunluluğu**: Tüm trafiği HTTPS’e zorlayın.
- **CORS**: Güvenli origin’lerle sınırlandırın.
- **Input Validation**: FluentValidation ile tüm istekleri doğrulayın.
- **JWT Authentication**: Token tabanlı kimlik doğrulama uygulayın.
- **Rate Limiting**: DDoS koruma politikaları ekleyin.

---

## Logging ve Monitoring
- **Microsoft.Extensions.Logging** altyapısı kullanılır.
- **Application Insights** veya **Prometheus/Grafana** ile izleme entegrasyonu yapılabilir.
- Health check endpoint’i ekleyin ve Kubernetes readiness/liveness probe olarak kullanın.

---

## Testler
- **Birim Testleri**: xUnit veya NUnit ile Service ve Repository katmanlarını test edin.
- **Entegrasyon Testleri**: In‑Memory DB veya Testcontainers kullanarak test ortamları oluşturun.
```bash
dotnet test
```

---

## CI/CD (GitHub Actions)
`.github/workflows/dotnet.yml` içeriği:
```yaml
name: .NET CI
on: [push, pull_request]
jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '9.0.x'
      - name: Restore & Build
        run: dotnet restore && dotnet build --no-restore -c Release
      - name: Test
        run: dotnet test --no-build --verbosity normal
      - name: Push to Docker Hub
        if: github.ref == 'refs/heads/main'
        run: |
          echo ${{ secrets.DOCKER_HUB_PASSWORD }} | docker login -u ${{ secrets.DOCKER_HUB_USERNAME }} --password-stdin
          docker build -t ${{ secrets.DOCKER_HUB_USERNAME }}/masraftakipapi:latest .
          docker push ${{ secrets.DOCKER_HUB_USERNAME }}/masraftakipapi:latest
```

---

## Katkıda Bulunma & Code of Conduct
1. Repo’yu fork edin.
2. Yeni bir branch oluşturun (`feature/...`).
3. Değişiklikleri commit edip push’layın.
4. Pull Request (PR) açın.
5. `.editorconfig` ve kod stiline uyun.
6. `CODE_OF_CONDUCT.md`’e uygun davranın.

---

## Lisans
Proje MIT lisansı altında dağıtılmaktadır. Detaylar için [LICENSE](LICENSE) dosyasını inceleyin.

---

## İletişim
Sorun, öneri veya katkı talepleri için **Issues** bölümünü kullanın:  
[https://github.com/aysavsar/MasrafTakipApi/issues](https://github.com/aysavsar/MasrafTakipApi/issues)

---

**Referanslar**  
[1] [RabbitMQ Tutorials](https://www.rabbitmq.com/tutorials?utm_source=chatgpt.com)  
[2] [Docker .NET Guide](https://docs.docker.com/guides/dotnet/containerize/?utm_source=chatgpt.com)  
[3] [EF Core Migrations](https://learn.microsoft.com/ef/core/managing-schemas/migrations/?utm_source=chatgpt.com)
