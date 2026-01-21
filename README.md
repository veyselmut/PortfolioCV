# 🌟 PortfolioCV - Modern Portfolio & CV Management System

[🇹🇷 Türkçe](#tr) | [🇬🇧 English](#en)

---

<a name="tr"></a>
## 🇹🇷 Türkçe

### 📋 Proje Hakkında

PortfolioCV, modern ve profesyonel bir portföy & CV yönetim sistemidir. Kişisel bilgilerinizi, eğitim geçmişinizi, iş deneyimlerinizi, projelerinizi ve becerilerinizi tek bir platformda yönetebilir ve ziyaretçilerinizle paylaşabilirsiniz.

### ✨ Özellikler

#### 🎨 Frontend (Public Site - PortfolioCV/)
- 🌐 Çok dilli destek (Türkçe/İngilizce)
- 🌓 Dark/Light mode
- 📱 Tam responsive tasarım
- ⚡ Yüksek performans
- 🎯 SEO optimize
- 📊 Ziyaretçi takibi
- 📧 İletişim formu
- 📄 PDF CV indirme

#### 🔧 Backend (Admin Panel - admin-panel/)
- 🔐 JWT tabanlı güvenli authentication
- 📊 Gelişmiş dashboard & analytics
- 🌍 IP bazlı ülke tespiti (bayrak gösterimi)
- 📈 Ziyaretçi istatistikleri
- 🎨 Modern UI/UX (Ant Design)
- 🌓 Dark/Light mode
- 📱 Responsive design
- 🔄 Real-time veri yönetimi

#### 📦 Yönetilebilir İçerikler
- 👤 Kişisel Bilgiler
- 🎓 Eğitim Geçmişi
- 💼 İş Deneyimleri
- 🛠️ Yetenekler
- 🚀 Projeler
- 🏆 Sertifikalar
- 🌐 Diller
- 👥 Referanslar
- 🔗 Sosyal Medya
- ⚙️ Hizmetler
- 💬 Mesajlar

### 🛠️ Teknoloji Stack'i

#### Frontend (Public Site - PortfolioCV/)
- **Framework:** ASP.NET Core 8.0 MVC
- **UI:** Razor Views, Bootstrap
- **Database:** SQL Server (Entity Framework Core)
- **Authentication:** Cookie-based
- **i18n:** Resource files (.resx)

#### Backend (Admin Panel - admin-panel/)
- **Framework:** React 19 + TypeScript
- **UI Library:** Ant Design 5.23
- **State Management:** Refine Framework
- **Build Tool:** Vite 6
- **HTTP Client:** Axios
- **i18n:** i18next
- **Icons:** Ant Design Icons, Flag Icons
- **Charts:** Ant Design Plots

#### Backend API
- **.NET:** ASP.NET Core 8.0 Web API
- **ORM:** Entity Framework Core
- **Database:** SQL Server
- **Authentication:** JWT Bearer
- **Email:** MailKit
- **PDF:** QuestPDF
- **Image Processing:** ImageSharp
- **Caching:** IMemoryCache
- **Compression:** Brotli/Gzip

### 📸 Ekran Görüntüleri

*(Screenshots buraya eklenebilir)*

### 🚀 Kurulum

#### Gereksinimler
- .NET 8.0 SDK
- Node.js 18+ (Admin panel için)
- SQL Server 2019+

#### 1. Repository'yi Klonlayın
```bash
git clone https://github.com/yourusername/PortfolioCV.git
cd PortfolioCV
```

#### 2. Database Ayarları
`appsettings.json` dosyasında connection string'i güncelleyin:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=vmDb;..."
  }
}
```

#### 3. Frontend (Public Site - PortfolioCV/) Çalıştırma
```bash
dotnet restore
dotnet run
```
Tarayıcıda: `http://localhost:5000`

#### 4. Admin Panel Kurulumu
```bash
cd admin-panel
npm install
npm run dev
```
Tarayıcıda: `http://localhost:5173`

### 🔧 Yapılandırma

#### Email Ayarları
`appsettings.json`:
```json
{
  "EmailSettings": {
    "Host": "smtp.yourdomain.com",
    "Port": 587,
    "Username": "your-email@domain.com",
    "Password": "your-password"
  }
}
```

#### CORS Ayarları
`Program.cs` içinde production domain'inizi ekleyin:
```csharp
policy.WithOrigins(
    "http://localhost:5173",
    "https://dashboard.yourdomain.com"
)
```

### 📦 Production Build

#### Frontend
```bash
dotnet publish -c Release -o ./publish
```

#### Admin Panel
```bash
cd admin-panel
npm run build
# dist/ klasörü oluşur
```

### 🌐 Deployment

#### Önerilen Yapı
- **Public Site:** `yourdomain.com` (Frontend + API)
- **Admin Panel:** `dashboard.yourdomain.com` (React SPA)

#### IIS Deployment
1. `publish/` klasörünü IIS'e kopyalayın
2. Application Pool: No Managed Code
3. `ASPNETCORE_ENVIRONMENT=Production` set edin

### 🔐 Güvenlik

- ✅ JWT Authentication
- ✅ CORS Protection
- ✅ SQL Injection Prevention (Parameterized Queries)
- ✅ XSS Protection
- ✅ HTTPS Enforcement
- ✅ Rate Limiting
- ✅ Input Validation

### 📝 Lisans

MIT License - Detaylar için [LICENSE](LICENSE) dosyasına bakın.

### 👨‍💻 Geliştirici

**Veysel Mut**
- Website: [veyselmut.com.tr](https://veyselmut.com.tr)
- Admin Panel: [dashboard.veyselmut.com.tr](https://dashboard.veyselmut.com.tr)

---

<a name="en"></a>
## 🇬🇧 English

### 📋 About The Project

PortfolioCV is a modern and professional portfolio & CV management system. You can manage your personal information, education history, work experiences, projects, and skills on a single platform and share them with your visitors.

### ✨ Features

#### 🎨 Frontend (Public Site - PortfolioCV/)
- 🌐 Multi-language support (Turkish/English)
- 🌓 Dark/Light mode
- 📱 Fully responsive design
- ⚡ High performance
- 🎯 SEO optimized
- 📊 Visitor tracking
- 📧 Contact form
- 📄 PDF CV download

#### 🔧 Admin Panel
- 🔐 JWT-based secure authentication
- 📊 Advanced dashboard & analytics
- 🌍 IP-based country detection (flag display)
- 📈 Visitor statistics
- 🎨 Modern UI/UX (Ant Design)
- 🌓 Dark/Light mode
- 📱 Responsive design
- 🔄 Real-time data management

#### 📦 Manageable Content
- 👤 Personal Information
- 🎓 Education History
- 💼 Work Experiences
- 🛠️ Skills
- 🚀 Projects
- 🏆 Certificates
- 🌐 Languages
- 👥 References
- 🔗 Social Media
- ⚙️ Services
- 💬 Messages

### 🛠️ Technology Stack

#### Frontend (Public Site - PortfolioCV/)
- **Framework:** ASP.NET Core 8.0 MVC
- **UI:** Razor Views, Bootstrap
- **Database:** SQL Server (Entity Framework Core)
- **Authentication:** Cookie-based
- **i18n:** Resource files (.resx)

#### Backend (Admin Panel - admin-panel/)
- **Framework:** React 19 + TypeScript
- **UI Library:** Ant Design 5.23
- **State Management:** Refine Framework
- **Build Tool:** Vite 6
- **HTTP Client:** Axios
- **i18n:** i18next
- **Icons:** Ant Design Icons, Flag Icons
- **Charts:** Ant Design Plots

#### Backend API
- **.NET:** ASP.NET Core 8.0 Web API
- **ORM:** Entity Framework Core
- **Database:** SQL Server
- **Authentication:** JWT Bearer
- **Email:** MailKit
- **PDF:** QuestPDF
- **Image Processing:** ImageSharp
- **Caching:** IMemoryCache
- **Compression:** Brotli/Gzip

### 📸 Screenshots

*(Screenshots can be added here)*

### 🚀 Installation

#### Requirements
- .NET 8.0 SDK
- Node.js 18+ (for Admin panel)
- SQL Server 2019+

#### 1. Clone Repository
```bash
git clone https://github.com/yourusername/PortfolioCV.git
cd PortfolioCV
```

#### 2. Database Configuration
Update connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=vmDb;..."
  }
}
```

#### 3. Run Frontend (Public Site - PortfolioCV/)
```bash
dotnet restore
dotnet run
```
Browser: `http://localhost:5000`

#### 4. Admin Panel Setup
```bash
cd admin-panel
npm install
npm run dev
```
Browser: `http://localhost:5173`

### 🔧 Configuration

#### Email Settings
In `appsettings.json`:
```json
{
  "EmailSettings": {
    "Host": "smtp.yourdomain.com",
    "Port": 587,
    "Username": "your-email@domain.com",
    "Password": "your-password"
  }
}
```

#### CORS Settings
Add your production domain in `Program.cs`:
```csharp
policy.WithOrigins(
    "http://localhost:5173",
    "https://dashboard.yourdomain.com"
)
```

### 📦 Production Build

#### Frontend
```bash
dotnet publish -c Release -o ./publish
```

#### Admin Panel
```bash
cd admin-panel
npm run build
# dist/ folder will be created
```

### 🌐 Deployment

#### Recommended Structure
- **Public Site:** `yourdomain.com` (Frontend + API)
- **Admin Panel:** `dashboard.yourdomain.com` (React SPA)

#### IIS Deployment
1. Copy `publish/` folder to IIS
2. Application Pool: No Managed Code
3. Set `ASPNETCORE_ENVIRONMENT=Production`

### 🔐 Security

- ✅ JWT Authentication
- ✅ CORS Protection
- ✅ SQL Injection Prevention (Parameterized Queries)
- ✅ XSS Protection
- ✅ HTTPS Enforcement
- ✅ Rate Limiting
- ✅ Input Validation

### 📝 License

MIT License - See [LICENSE](LICENSE) file for details.

### 👨‍💻 Developer

**Veysel Mut**
- Website: [veyselmut.com.tr](https://veyselmut.com.tr)
- Admin Panel: [dashboard.veyselmut.com.tr](https://dashboard.veyselmut.com.tr)

---

## 🙏 Acknowledgments

- [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet)
- [React](https://react.dev/)
- [Ant Design](https://ant.design/)
- [Refine](https://refine.dev/)
- [QuestPDF](https://www.questpdf.com/)
- [Flag Icons](https://github.com/lipis/flag-icons)

---

**⭐ If you like this project, please give it a star!**
