# Microservices Demo - Başlatma Kılavuzu

## 🚀 Hızlı Başlatma

### Tüm Servisleri Başlatmak İçin:

```bash
start-all.bat
```

Bu dosya şunları başlatır:
1. **ApiGateway** - Port 7239 (HTTPS)
2. **ProductService** - Port 5265
3. **OrchestratorService** - Port 7007 (HTTPS)
4. **Angular Frontend** - Port 4200

Her servis için ayrı bir terminal penceresi açılır.

### Tüm Servisleri Durdurmak İçin:

```bash
stop-all.bat
```

---

## 📝 Manuel Başlatma

### Backend Servisleri:

```bash
# ApiGateway
cd ApiGateway
dotnet run

# ProductService
cd ProductService
dotnet run

# OrchestratorService
cd OrchestratorService
dotnet run
```

### Frontend:

```bash
cd frontend
npm start
```

---

## 🌐 URL'ler

- **Frontend:** http://localhost:4200
- **Backend Swagger:** https://localhost:7239/swagger
- **ApiGateway:** https://localhost:7239/gateway
- **OrchestratorService:** https://localhost:7007

---

## ⚠️ Önemli Notlar

1. İlk kez çalıştırırken frontend dependencies yüklenmeli:
   ```bash
   cd frontend
   npm install
   ```

2. Backend SSL sertifika hatası alırsanız:
   ```bash
   dotnet dev-certs https --trust
   ```

3. Port 4200, 7007, 7239 veya 5265 meşgulse önce stop-all.bat çalıştırın.

---

## 🔧 Sorun Giderme

**Servisler başlamazsa:**
- Visual Studio'dan "Multiple Startup Projects" kullanın
- stop-all.bat ile tüm servisleri durdurup yeniden başlatın

**CORS hatası alırsanız:**
- Backend servislerinin Program.cs dosyalarına CORS policy eklenmiş olmalı
- ApiGateway'in çalıştığından emin olun

**Frontend backend'e bağlanamazsa:**
- Backend servislerinin çalıştığını kontrol edin
- https://localhost:7239/swagger adresini açıp API'lerin çalıştığını doğrulayın
