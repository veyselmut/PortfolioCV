# Portfolio CV - Deployment Guide (MSSQL Version)

Projeniz MSSQL altyapısı ile yayına hazır hale getirilmiştir.

## 1. Dosya Lokasyonu
Yayınlanacak dosyalar şu klasörde oluşturulmuştur:
`./publish`

Bu klasörün içeriğini sunucunuza yükleyin.

## 2. Veritabanı (MSSQL)
Uygulama artık SQL Server kullanmaktadır.
- **Connection String:** Sunucudaki `appsettings.json` dosyasını açın ve `DefaultConnection` kısmını sunucunuzdaki SQL veritabanına göre düzenleyin.
- **Migration:** Uygulama "Code First" yaklaşımını kullandığı için, veritabanı tabloları uygulama ilk çalıştığında (Program.cs içinde `EnsureCreated` veya `Migrate` çağrılmışsa) veya manuel olarak `dotnet ef database update` ile oluşturulmalıdır.
  *Not: Mevcut kodda `context.Database.EnsureCreated()` kullanılmaktadır, bu da veritabanı yoksa tabloları oluşturur.*

## 3. Klasör İzinleri (ÖNEMLİ)
Resim yüklemeleri için sunucuda aşağıdaki klasöre **YAZMA (WRITE)** izni verilmelidir:
`wwwroot/uploads`

## 4. Admin Bilgileri
- **Kullanıcı Adı:** praimkepa
- **Şifre:** 3408v+-0

## 5. Çalıştırma
```bash
dotnet PortfolioCV.dll
```
IIS kullanıyorsanız Application Pool'un "No Managed Code" değil, ".NET Core" destekli olduğundan emin olun (gerçi modern IIS'de 'No Managed Code' + AspNetCoreModuleV2 kullanılır).
