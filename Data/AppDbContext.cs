using Microsoft.EntityFrameworkCore;
using PortfolioCV.Models;

namespace PortfolioCV.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<WelcomeMessage> WelcomeMessages { get; set; }
    public DbSet<PersonalInfo> PersonalInfos { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<CvReference> References { get; set; }
    public DbSet<SocialMedia> SocialMedias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed default admin
        modelBuilder.Entity<Admin>().HasData(
            new Admin { Id = 1, Username = "admin", Password = "admin123", Email = "admin@portfolio.com" }
        );

        // Seed default welcome message
        modelBuilder.Entity<WelcomeMessage>().HasData(
            new WelcomeMessage { Id = 1, Title = "Merhaba, Ben Veysel Mut!", Subtitle = "Mekatronik Mühendisi & Yazılım Geliştirici", Description = ".NET Teknolojileri ve Modern Web Çözümleri üzerine uzmanlaşmış dijital portfolyoma hoş geldiniz." }
        );

        // Seed default social media
        modelBuilder.Entity<SocialMedia>().HasData(
            new SocialMedia { Id = 1, LinkedIn = "https://linkedin.com", Github = "https://github.com" }
        );
        
        // Seed default personal info
        modelBuilder.Entity<PersonalInfo>().HasData(
            new PersonalInfo 
            { 
                Id = 1, 
                FullName = "Veysel Mut", 
                Title = "Mekatronik Mühendisi / Yazılım Geliştirici", 
                Email = "veysel.mut@hotmail.com", 
                Phone = "+90 (531) 229 74 05", 
                Address = "Kavakpınar Mah. Zeytinli Cad. No:35/2 Pendik / İstanbul", 
                Gender = "Erkek", 
                MilitaryStatus = "Yapıldı",
                HasDrivingLicense = true,
                DrivingLicenseClass = "B",
                About = "Mekatronik mühendisliği mezunu, yazılım geliştirme tutkunu bir profesyonelim. .NET Core, C# ve modern web teknolojileri üzerine uzmanlaşmaktayım."
            }
        );

        // Seed Educations
        modelBuilder.Entity<Education>().HasData(
            new Education { Id = 1, EducationType = "Lisans", School = "Karabük Üniversitesi", Department = "Mekatronik Mühendisliği", StartDate = new DateTime(2012, 9, 1), EndDate = new DateTime(2017, 10, 1), IsContinuing = false, GradeSystem = "4", Grade = 2.91, Order = 1 },
            new Education { Id = 2, EducationType = "Ön Lisans", School = "Anadolu Üniversitesi", Department = "Bilgisayar Programcılığı", StartDate = new DateTime(2022, 10, 1), IsContinuing = true, Order = 2 },
            new Education { Id = 3, EducationType = "Ön Lisans", School = "Atatürk Üniversitesi", Department = "İş Sağlığı ve Güvenliği", StartDate = new DateTime(2018, 8, 1), IsContinuing = true, Order = 3 },
            new Education { Id = 4, EducationType = "Lise", School = "Faruk Nafiz Çamlıbel Anadolu İmam Hatip Lisesi", StartDate = new DateTime(2007, 8, 1), EndDate = new DateTime(2011, 6, 1), IsContinuing = false, Order = 4 },
            new Education { Id = 5, EducationType = "İlköğretim", School = "Darüşşafaka Eğitim Kurumları", StartDate = new DateTime(2001, 1, 1), EndDate = new DateTime(2007, 1, 1), IsContinuing = false, Order = 5 }
        );

        // Seed Experiences
        modelBuilder.Entity<Experience>().HasData(
            new Experience { Id = 1, Position = "Proje Geliştirici", CompanyName = "Bireysel", Industry = "Yazılım", StartDate = new DateTime(2021, 7, 1), IsCurrentlyWorking = true, Order = 1, Address = "İstanbul" },
            new Experience { Id = 2, Position = "Freelance Geliştirici", CompanyName = "PYS Geliştirmesi", Industry = "Yazılım", StartDate = new DateTime(2022, 12, 1), EndDate = new DateTime(2023, 4, 1), IsCurrentlyWorking = false, Order = 2 },
            new Experience { Id = 3, Position = "Web Geliştirici", CompanyName = "E-Ticaret Sitesi", Industry = "E-Ticaret", StartDate = new DateTime(2022, 4, 1), EndDate = new DateTime(2023, 1, 1), IsCurrentlyWorking = false, Order = 3 },
            new Experience { Id = 4, Position = "Stajyer Mühendis", CompanyName = "Tork Mühendislik Asansör", Industry = "Asansör", StartDate = new DateTime(2017, 7, 1), EndDate = new DateTime(2017, 8, 1), IsCurrentlyWorking = false, Order = 4 },
            new Experience { Id = 5, Position = "Stajyer Mühendis", CompanyName = "Geta Grup Asansör", Industry = "Asansör", StartDate = new DateTime(2017, 6, 1), EndDate = new DateTime(2017, 7, 1), IsCurrentlyWorking = false, Order = 5 }
        );

        // Seed Skills
        modelBuilder.Entity<Skill>().HasData(
            new Skill { Id = 1, Name = "C# / ASP.NET Core", Level = 85, Order = 1 },
            new Skill { Id = 2, Name = "MS SQL Server", Level = 80, Order = 2 },
            new Skill { Id = 3, Name = "Entity Framework Core", Level = 85, Order = 3 },
            new Skill { Id = 4, Name = "HTML / CSS / JS", Level = 75, Order = 4 },
            new Skill { Id = 5, Name = "Git / Docker", Level = 70, Order = 5 }
        );

        // Seed Languages
        modelBuilder.Entity<Language>().HasData(
            new Language { Id = 1, Name = "İngilizce", Level = "B2", Order = 1 },
            new Language { Id = 2, Name = "Türkçe", Level = "C2", Order = 2 }
        );

        // Seed Certificates
        modelBuilder.Entity<Certificate>().HasData(
            new Certificate { Id = 1, Name = "Asp.Net Core 8.0 ile Sıfırdan İleri Seviye Web Geliştirme", Issuer = "Udemy", Date = new DateTime(2020, 6, 8), Order = 1 },
            new Certificate { Id = 2, Name = "Komple Uygulamalı Web Geliştirme Eğitimi", Issuer = "Udemy", Date = new DateTime(2017, 11, 8), Order = 2 },
            new Certificate { Id = 3, Name = "Python ile Sıfırdan İleri Seviye Programlama", Issuer = "Udemy", Date = new DateTime(2024, 1, 26), Order = 3 }
        );

        // Seed Projects
        modelBuilder.Entity<Project>().HasData(
            new Project { 
                Id = 1, 
                Name = ".NET Core Tabanlı Bireysel Portföy Sitesi", 
                Description = "ASP.NET Core 8.0 MVC ile geliştirilmiş modern portföy ve CV sitesi",
                Tasks = "Frontend tasarım\nBackend geliştirme\nVeritabanı yönetimi\nAdmin panel entegrasyonu",
                Date = new DateTime(2023, 7, 1), 
                Url = "https://github.com", 
                Order = 1 
            },
            new Project { 
                Id = 2, 
                Name = "ASP.NET 6 Tabanlı E-Ticaret Sitesi", 
                Description = "Kapsamlı e-ticaret platformu",
                Tasks = "Ürün yönetimi\nSepet sistemi\nÖdeme entegrasyonu",
                Date = new DateTime(2022, 12, 1), 
                Order = 2 
            },
            new Project { 
                Id = 3, 
                Name = "Personel Yönetim Sistemi (PYS)", 
                Description = "Kurumsal personel takip ve yönetim sistemi",
                Tasks = "Personel kayıt\nİzin yönetimi\nRaporlama",
                Date = new DateTime(2022, 4, 1), 
                Order = 3 
            }
        );

        // Seed References
        modelBuilder.Entity<CvReference>().HasData(
            new CvReference { Id = 1, Name = "Nebi Erdal Akkabak", Company = "Vakıf Katılım", Position = "Bireysel Portföy Müdürü", Phone = "+90 (537) 363 18 15", Order = 1 },
            new CvReference { Id = 2, Name = "Fatih Yılmaz", Company = "ODELLO OTOMOTİV", Position = "Kalıphane Mühendisi", Email = "m.fatihyilmaz@outlook.com", Phone = "+90 (539) 947 47 59", Order = 2 },
            new CvReference { Id = 3, Name = "İsa Demir", Company = "Softtech A.Ş.", Position = "Ürün Mimarı", Phone = "+90 (535) 027 29 47", Order = 3 }
        );
    }
}
