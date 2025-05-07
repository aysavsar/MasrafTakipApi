8 saniye için düşündüm


````markdown
# MasrafTakipApi

**MasrafTakipApi**, .NET 9 ve ASP.NET Core tabanlı, katmanlı mimarisi sayesinde sürdürülebilir, test edilebilir ve genişletilebilir bir harcama takip Web API’sidir :contentReference[oaicite:0]{index=0}.  
Entity Framework Core’un Code‑First migrations özelliği ile model değişiklikleri veritabanı şemasına otomatik yansıtılır :contentReference[oaicite:1]{index=1}.  
Asenkron işlemler için RabbitMQ mesaj kuyruğu kullanılır; yönetim arayüzü `http://localhost:15672` adresinden erişilebilir :contentReference[oaicite:2]{index=2}.  
Docker Compose ile API, PostgreSQL ve RabbitMQ servisleri tek komutla ayağa kaldırılabilir :contentReference[oaicite:3]{index=3}.  
Swagger/OpenAPI (Swashbuckle) entegre edilerek interaktif dokümantasyon ve test arayüzü sunulur :contentReference[oaicite:4]{index=4}.  
Proje, GitHub Actions CI/CD, kapsamlı logging, versiyonlama, güvenlik ve katkı rehberi bölümleriyle tam bir “rapor” niteliğindedir :contentReference[oaicite:5]{index=5}.

---

## 📊 İçindekiler  
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
**MasrafTakipApi**, kuruluşların ve bireylerin harcamalarını merkezi bir şekilde takip edip yönetmelerini sağlar :contentReference[oaicite:6]{index=6}.  
Controller → Service → Repository katmanlı mimarisiyle sorumluluklar ayrılmış, temiz kod ve test edilebilirlik garanti edilir :contentReference[oaicite:7]{index=7}.  
Entity Framework Core kod‑ilk (Code‑First) migrasyonları, model ve veritabanı şemasını senkron tutar :contentReference[oaicite:8]{index=8}.  
RabbitMQ üzerinden asenkron kuyruk işlemleri gerçekleştirilir ve `expense-queue` kuyruk ismi kullanılır :contentReference[oaicite:9]{index=9}.  
Docker Compose tanımı sayesinde tüm bileşenler tek komutla ayağa kalkar :contentReference[oaicite:10]{index=10}.  

---

## Öne Çıkan Özellikler  
- **CRUD Operasyonları**: `/api/expenses` uç noktalarıyla GET, POST, PUT, DELETE işlemleri yapılabilir :contentReference[oaicite:11]{index=11}.  
- **Katmanlı Mimari**: Controller, Service ve Repository ayrımıyla kod bakımı ve testi kolaylaşır :contentReference[oaicite:12]{index=12}.  
- **EF Core Migrations**: Kod–ilk yaklaşımıyla veritabanı şemasını güncel tutar :contentReference[oaicite:13]{index=13}.  
- **RabbitMQ Entegrasyonu**: AMQP 0‑9‑1 ile güvenilir mesajlaşma sağlar :contentReference[oaicite:14]{index=14}.  
- **Swagger/OpenAPI**: Swashbuckle ile otomatik dokümantasyon :contentReference[oaicite:15]{index=15}.  
- **Docker Compose**: Tek adımlı ortam kurulumunu destekler :contentReference[oaicite:16]{index=16}.  
- **AutoMapper**: DTO ve Entity dönüşümlerini merkezi olarak yönetir :contentReference[oaicite:17]{index=17}.  
- **FluentValidation**: İstek doğrulamalarını güçlü koşullarla tanımlama imkânı sunar :contentReference[oaicite:18]{index=18}.  
- **Logging**: Microsoft.Extensions.Logging altyapısıyla kapsamlı loglama :contentReference[oaicite:19]{index=19}.  
- **Versiyonlama**: API versioning stratejileriyle geriye dönük uyumluluk sağlanır :contentReference[oaicite:20]{index=20}.  
- **CI/CD**: GitHub Actions ile otomatik build, test ve deploy iş akışları :contentReference[oaicite:21]{index=21}.  

---

## Kullanılan Teknolojiler  
- **.NET 9 / ASP.NET Core Web API** :contentReference[oaicite:22]{index=22}  
- **Entity Framework Core** :contentReference[oaicite:23]{index=23}  
- **PostgreSQL**  
- **RabbitMQ v3.9+** :contentReference[oaicite:24]{index=24}  
- **Docker & Docker Compose** :contentReference[oaicite:25]{index=25}  
- **Swashbuckle (Swagger/OpenAPI)** :contentReference[oaicite:26]{index=26}  
- **AutoMapper** :contentReference[oaicite:27]{index=27}  
- **FluentValidation** :contentReference[oaicite:28]{index=28}  
- **GitHub Actions** :contentReference[oaicite:29]{index=29}  
- **Microsoft.Extensions.Logging** :contentReference[oaicite:30]{index=30}  

---

## Önkoşullar  
1. **.NET 9 SDK** (`dotnet --version >= 9.0`) :contentReference[oaicite:31]{index=31}  
2. **Docker Engine & Compose V2+** :contentReference[oaicite:32]{index=32}  
3. **RabbitMQ** (Management Plugin aktif, `http://localhost:15672`) :contentReference[oaicite:33]{index=33}  
4. Tercihe bağlı: **pgAdmin 4** veya başka bir SQL istemcisi.  

---

## Kurulum ve Çalıştırma  
```bash
git clone https://github.com/aysavsar/MasrafTakipApi.git   # Depoyu klonla
cd MasrafTakipApi                                         # Klasöre gir
dotnet restore                                            # Paketleri indir
dotnet build                                              # Projeyi derle
dotnet run --launch-profile "Development"                 # Uygulamayı başlat
````

Uygulama `https://localhost:5001` adresinde hizmet verir.
Swagger UI: `https://localhost:5001/swagger/index.html` ([RabbitMQ][1])

---

## Konfigürasyon & Ortam Değişkenleri

**appsettings.json**:

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

Hassas verileri GitHub Secrets veya ortam değişkenleriyle yönetin ([Docker Documentation][2]).

---

## Veritabanı & Migrations

Model değişikliklerinin veritabanına yansıması için:

```bash
dotnet ef migrations add InitialCreate   # Migration oluştur
dotnet ef database update               # Veritabanını güncelle
```

Bu adımları her model güncellemesinde tekrarlayarak şemayı güncel tutun ([RabbitMQ][3]).

---

## RabbitMQ Mesaj Kuyruğu

* **Management UI:** `http://localhost:15672` (guest/guest) ([RabbitMQ][4])
* **Kuyruk Adı:** `expense-queue`
* `IRabbitMQService` ile publish/consume işlemleri soyutlanmıştır.
* `RabbitMQPublisher` ve `RabbitMQConsumer` sınıfları asenkron iş akışları sağlar ([RabbitMQ][5]).

---

## Docker Compose ile Orkestrasyon

```bash
docker-compose up -d   # API, DB ve RabbitMQ’yu başlat
```

**docker-compose.yml**:

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

* **API:** `https://localhost:5001`
* **DB:** `localhost:5432`
* **RabbitMQ UI:** `http://localhost:15672` ([Docker Documentation][2])

---

## API Dokümantasyonu (Swagger)

Swashbuckle entegrasyonu:

```csharp
builder.Services.AddSwaggerGen();
app.UseSwagger();
app.UseSwaggerUI();
```

`/swagger/index.html` üzerinden tüm endpoint’leri keşfedin ve test edin ([RabbitMQ][1]).

---

## Versiyonlama

API versioning kütüphanesi ile URL veya header tabanlı versiyonlama stratejileri uygulayın ([Docker Documentation][6]).
`.AddApiVersioning()` konfigürasyonunu ekleyin.

---

## Güvenlik En İyi Uygulamaları

* **HTTPS Zorunluluğu:** Tüm trafiği HTTPS’e zorlayın ([Docker Documentation][7]).
* **CORS:** Güvenli origin’lerle sınırlandırın ([Docker Documentation][7]).
* **Input Validation:** FluentValidation ile tüm istekleri doğrulayın ([Docker Documentation][8]).
* **JWT Authentication:** Token tabanlı kimlik doğrulama uygulayın ([Docker Documentation][9]).
* **Rate Limiting:** DDoS koruma politikaları ekleyin.

---

## Logging ve Monitoring

* **Microsoft.Extensions.Logging** altyapısını kullanın ([Docker Documentation][10]).
* **Application Insights** veya **Prometheus/Grafana** ile izleme entegrasyonu yapın ([Docker Documentation][9]).
* **Health check** endpoint’i ekleyin ve Kubernetes readiness/liveness probe olarak kullanın.

---

## Testler

* **Birim Testleri:** xUnit veya NUnit ile Service ve Repository katmanlarını test edin.
* **Entegrasyon Testleri:** In‑Memory DB veya Testcontainers kullanarak test ortamları oluşturun.

```bash
dotnet test
```

Testleri container içinde çalıştırmak için Docker Compose Watch özelliğini kullanabilirsiniz ([Docker Documentation][11]).

---

## CI/CD (GitHub Actions)

**.github/workflows/dotnet.yml**:

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

Her PR ve `main` push’unda otomatik build, test ve Docker Hub’a push işlemleri gerçekleşir ([Docker Documentation][10]).

---

## Katkıda Bulunma & Code of Conduct

1. Repo’yu **fork** edin.
2. Yeni bir branch oluşturun (`feature/...`).
3. Değişiklikleri commit edip push’layın.
4. Pull Request (PR) açın.
5. `.editorconfig` ve kod stiline uyun.
6. `CODE_OF_CONDUCT.md`’e uygun davranın.

---


## İletişim

Sorun, öneri veya katkı talepleri için [Issues](https://github.com/aysavsar/MasrafTakipApi/issues) bölümünü kullanın.

[1]: https://www.rabbitmq.com/tutorials/tutorial-two-dotnet?utm_source=chatgpt.com "RabbitMQ tutorial - Work Queues"
[2]: https://docs.docker.com/guides/dotnet/containerize/?utm_source=chatgpt.com "Containerize a .NET application - Docker Docs"
[3]: https://www.rabbitmq.com/tutorials/tutorial-three-dotnet?utm_source=chatgpt.com "RabbitMQ tutorial - Publish/Subscribe"
[4]: https://www.rabbitmq.com/tutorials?utm_source=chatgpt.com "RabbitMQ Tutorials"
[5]: https://www.rabbitmq.com/tutorials/tutorial-one-dotnet?utm_source=chatgpt.com "RabbitMQ tutorial - \"Hello World!\""
[6]: https://docs.docker.com/reference/samples/mysql/?utm_source=chatgpt.com "MySQL samples - Docker Docs"
[7]: https://docs.docker.com/reference/cli/docker/init/?utm_source=chatgpt.com "docker init - Docker Docs"
[8]: https://docs.docker.com/reference/samples/javascript/?utm_source=chatgpt.com "JavaScript samples - Docker Docs"
[9]: https://docs.docker.com/compose/how-tos/file-watch/?utm_source=chatgpt.com "Use Compose Watch - Docker Docs"
[10]: https://docs.docker.com/guides/dotnet/configure-ci-cd/?utm_source=chatgpt.com "Configure CI/CD for your .NET application - Docker Docs"
[11]: https://docs.docker.com/guides/dotnet/develop/?utm_source=chatgpt.com "Use containers for .NET development - Docker Docs"
