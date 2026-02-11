# Microservices Demo Project

Bu proje, modern mikroservis mimarisini, .NET 10 ve Angular 18 teknolojilerini kullanarak gösteren kapsamlı bir demo uygulamasıdır. Proje, API Gateway, Orchestrator deseni ve Clean Architecture prensiplerini içermektedir.

## 🚀 Proje Hakkında

MicroservicesDemo, e-ticaret benzeri bir senaryo üzerinden mikroservislerin nasıl iletişim kurduğunu, verilerin nasıl yönetildiğini ve modern bir frontend arayüzü ile nasıl sunulduğunu simüle eder.

### Temel Özellikler

*   **Mikroservis Mimarisi:** Servislerin bağımsız olarak geliştirilebilir ve dağıtılabilir yapısı.
*   **API Gateway (Ocelot):** İstemciler için tek bir giriş noktası sağlar ve istekleri ilgili servislere yönlendirir.
*   **Orchestrator Pattern:** Karmaşık iş süreçlerini yönetmek için servisler arası iletişimi koordine eder.
*   **Clean Architecture:** Katmanlı ve test edilebilir kod yapısı.
*   **Modern Frontend:** Angular 18 ve Angular Material ile geliştirilmiş responsive ve şık kullanıcı arayüzü.

## 🏗️ Mimari

Proje aşağıdaki temel bileşenlerden oluşur:

graph TD
    User[Kullanıcı / Frontend] -->|HTTPS| Gateway[ApiGateway (Ocelot)]
    Gateway -->|/gateway| Orchestrator[Orchestrator Service]
    Gateway -->|/api| Product[Product Service]
    
    Orchestrator -->|HTTP / Refit| Product
    
    Product -->|EF Core| DB[(SQL Server)]

*   **ApiGateway:** Tüm dış istekleri karşılar.
*   **OrchestratorService:** İş mantığını ve servisler arası akışı yönetir (örneğin, ürün oluşturma süreçleri). `Refit` kullanarak diğer servislerle haberleşir.
*   **ProductService:** Ürün verilerinin yönetiminden sorumludur (CRUD). Veritabanı işlemlerini `EF Core` ile, iç mantığı `MediatR` ile yönetir.
*   **Frontend:** Angular tabanlı Single Page Application (SPA).

## 🛠️ Teknolojiler

### Backend (.NET 10)
*   **ASP.NET Core Web API:** RESTful servisler.
*   **Ocelot:** API Gateway çözümü.
*   **Refit:** Tip güvenli HTTP istemcisi (microservice communication).
*   **MediatR:** In-process messaging ve CQRS implementasyonu.
*   **Entity Framework Core:** ORM ve veritabanı erişimi.
*   **AutoMapper:** Nesne eşleme.
*   **Swagger/OpenAPI:** API dokümantasyonu.
*   **SQL Server:** İlişkisel veritabanı.

### Frontend (Angular 18)
*   **Angular CLI:** Proje yönetimi.
*   **TypeScript:** Statik tipli JavaScript.
*   **Angular Material:** UI bileşen kütüphanesi.
*   **RxJS:** Reaktif programlama.
*   **Chart.js / ng2-charts:** Veri görselleştirme ve grafikler.

## ⚙️ Kurulum ve Çalıştırma

### Gereksinimler
*   [.NET 10 SDK](https://dotnet.microsoft.com/download) (veya en son preview sürümü)
*   [Node.js](https://nodejs.org/) (LTS sürümü önerilir)
*   [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB veya Express)
*   Angular CLI: `npm install -g @angular/cli`

### Projeyi İndirme

```bash
git clone https://github.com/kullaniciadi/MicroservicesDemo.git
cd MicroservicesDemo
```

### Hızlı Başlatma (Windows)

Projedeki tüm servisleri ve frontend'i tek komutla başlatmak için kök dizindeki bat dosyasını kullanabilirsiniz:

```bash
start-all.bat
```

Bu komut:
1.  **ApiGateway**'i başlatır.
2.  **ProductService**'i başlatır.
3.  **OrchestratorService**'i başlatır.
4.  **Frontend** uygulamasını başlatır.

Durdurmak için:
```bash
stop-all.bat
```

### Manuel Kurulum

Eğer servisleri tek tek başlatmak isterseniz:

**1. Veritabanı Migration (İlk kurulumda):**
```bash
cd ProductService
dotnet ef database update
```

**2. Backend Servisleri:**
Her servis klasörüne gidip (ApiGateway, OrchestratorService, ProductService) aşağıdaki komutu çalıştırın:
```bash
dotnet run
```

**3. Frontend:**
```bash
cd frontend
npm install
npm start
```

## 📚 Dokümantasyon ve URL'ler

Servisler ayağa kalktığında aşağıdaki adreslerden erişilebilir:

*   **Frontend Uygulaması:** [http://localhost:4200](http://localhost:4200)
*   **Api Gateway:** [https://localhost:7239](https://localhost:7239)
*   **Orchestrator Service:** [https://localhost:7007/swagger](https://localhost:7007/swagger)
*   **Product Service:** [http://localhost:5265/swagger](http://localhost:5265/swagger)

## 🤝 Katkıda Bulunma

1.  Bu depoyu forklayın (Fork).
2.  Yeni bir özellik dalı oluşturun (`git checkout -b ozellik/YeniOzellik`).
3.  Değişikliklerinizi commit edin (`git commit -m 'Yeni özellik eklendi'`).
4.  Dalınızı pushlayın (`git push origin ozellik/YeniOzellik`).
5.  Bir Pull Request oluşturun.

## 📄 Lisans

Bu proje [MIT Lisansı](LICENSE) altında lisanslanmıştır.
