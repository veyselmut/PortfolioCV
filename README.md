# 🎨 PortfolioCV - Modern Portfolio & CV Management System

A professional, multilingual portfolio and CV management system built with **ASP.NET Core 8**, featuring a powerful admin panel and beautiful, responsive design.

## ✨ Features

### 🌐 Frontend
- **Multilingual Support**: Turkish & English with easy language switching
- **Theme Support**: Light, Dark, and System theme modes
- **Responsive Design**: Perfect on all devices
- **Modern UI**: Clean, professional design with smooth animations
- **SEO Optimized**: Proper meta tags and semantic HTML

### 🔐 Admin Panel
- **Secure Authentication**: Cookie-based authentication
- **Complete CRUD Operations**: Manage all content easily
- **Rich Content Management**:
  - Personal Information
  - Education History
  - Work Experience
  - Skills & Languages
  - Projects & Certifications
  - References
  - Social Media Links
- **Modern Dashboard**: Beautiful AdminLTE-based interface
- **Multilingual Admin**: Admin panel available in TR/EN

### 🛠️ Technical Features
- **ASP.NET Core 8**: Latest .NET framework
- **Entity Framework Core**: Code-first database approach
- **SQL Server**: Robust database backend
- **Localization**: Full i18n support
- **CSRF Protection**: Secure form submissions
- **Responsive Images**: Optimized image handling

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- SQL Server (LocalDB, Express, or Full)
- Visual Studio 2022 or VS Code (optional)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/YOUR_USERNAME/PortfolioCV.git
   cd PortfolioCV
   ```

2. **Update Connection String**
   
   Edit `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=.\\SQLEXPRESS;Database=PortfolioCV;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
     }
   }
   ```

3. **Run the Application**
   ```bash
   dotnet restore
   dotnet run
   ```

4. **Access the Application**
   - Frontend: `https://localhost:5001`
   - Admin Panel: `https://localhost:5001/Admin/Login`

### Default Admin Credentials
- **Username**: `praimkepa`
- **Password**: `3408v+-0`

⚠️ **Important**: Change these credentials immediately after first login!

## 📁 Project Structure

```
PortfolioCV/
├── Controllers/         # MVC Controllers
├── Models/             # Data models
├── Views/              # Razor views
│   ├── Home/          # Frontend views
│   ├── Admin/         # Admin panel views
│   └── Shared/        # Shared layouts
├── ViewComponents/     # Reusable view components
├── Resources/          # Localization files
├── wwwroot/           # Static files
│   ├── css/          # Stylesheets
│   ├── js/           # JavaScript files
│   └── uploads/      # User uploads
├── Data/              # Database context
└── Helpers/           # Utility classes
```

## 🌍 Localization

The application supports Turkish and English. To add a new language:

1. Create resource files in `Resources/` folder
2. Add language option in `Program.cs`
3. Update language switcher in layouts

## 🎨 Customization

### Changing Theme Colors
Edit CSS variables in `wwwroot/css/site.css`:
```css
:root {
    --primary-color: #your-color;
    --secondary-color: #your-color;
}
```

### Adding New Sections
1. Create model in `Models/`
2. Add DbSet in `AppDbContext.cs`
3. Create migration: `dotnet ef migrations add YourMigration`
4. Update database: `dotnet ef database update`

## 📦 Deployment

### For Windows Server with IIS

1. **Publish the application**
   ```bash
   dotnet publish -c Release -o ./publish --self-contained false
   ```

2. **Update `appsettings.Production.json`** with production database credentials

3. **Upload files** to your server

4. **Configure IIS**:
   - Create new website
   - Set application pool to "No Managed Code"
   - Point to published folder

5. **Set environment variable** in `web.config`:
   ```xml
   <environmentVariable name="ASPNETCORE_ENVIRONMENT" value="Production" />
   ```

### For Linux with Nginx

See [Microsoft's deployment guide](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx)

## 🔒 Security Notes

- Never commit `appsettings.Production.json` to version control
- Change default admin credentials immediately
- Use strong passwords
- Enable HTTPS in production
- Keep dependencies updated

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🙏 Acknowledgments

- [AdminLTE](https://adminlte.io/) - Admin panel template
- [Font Awesome](https://fontawesome.com/) - Icons
- [Flagcdn](https://flagcdn.com/) - Flag images
- [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet) - Framework

## 📧 Contact

For questions or support, please open an issue on GitHub.

---

Made with ❤️ using ASP.NET Core 8
