using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PortfolioCV.Migrations
{
    /// <inheritdoc />
    public partial class InitialMsSql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Issuer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EducationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeachingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    School = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Faculty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsContinuing = table.Column<bool>(type: "bit", nullable: false),
                    GradeSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<double>(type: "float", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Industry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCurrentlyWorking = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasDrivingLicense = table.Column<bool>(type: "bit", nullable: false),
                    DrivingLicenseClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MilitaryStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tasks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkedIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instagram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Youtube = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Github = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WelcomeMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WelcomeMessages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "Password", "Username" },
                values: new object[] { 1, "admin@portfolio.com", "admin123", "admin" });

            migrationBuilder.InsertData(
                table: "Certificates",
                columns: new[] { "Id", "Date", "Description", "Issuer", "Name", "Order" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Udemy", "Asp.Net Core 8.0 ile Sıfırdan İleri Seviye Web Geliştirme", 1 },
                    { 2, new DateTime(2017, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Udemy", "Komple Uygulamalı Web Geliştirme Eğitimi", 2 },
                    { 3, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Udemy", "Python ile Sıfırdan İleri Seviye Programlama", 3 }
                });

            migrationBuilder.InsertData(
                table: "Educations",
                columns: new[] { "Id", "Department", "EducationType", "EndDate", "Faculty", "Grade", "GradeSystem", "IsContinuing", "Order", "School", "StartDate", "TeachingType" },
                values: new object[,]
                {
                    { 1, "Mekatronik Mühendisliği", "Lisans", new DateTime(2017, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2.9100000000000001, "4", false, 1, "Karabük Üniversitesi", new DateTime(2012, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, "Bilgisayar Programcılığı", "Ön Lisans", null, null, null, null, true, 2, "Anadolu Üniversitesi", new DateTime(2022, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, "İş Sağlığı ve Güvenliği", "Ön Lisans", null, null, null, null, true, 3, "Atatürk Üniversitesi", new DateTime(2018, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, null, "Lise", new DateTime(2011, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, 4, "Faruk Nafiz Çamlıbel Anadolu İmam Hatip Lisesi", new DateTime(2007, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 5, null, "İlköğretim", new DateTime(2007, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, 5, "Darüşşafaka Eğitim Kurumları", new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "Id", "Address", "CompanyName", "Department", "EndDate", "Industry", "IsCurrentlyWorking", "Order", "Position", "StartDate" },
                values: new object[,]
                {
                    { 1, "İstanbul", "Bireysel", null, null, "Yazılım", true, 1, "Proje Geliştirici", new DateTime(2021, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, null, "PYS Geliştirmesi", null, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yazılım", false, 2, "Freelance Geliştirici", new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, null, "E-Ticaret Sitesi", null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "E-Ticaret", false, 3, "Web Geliştirici", new DateTime(2022, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, null, "Tork Mühendislik Asansör", null, new DateTime(2017, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Asansör", false, 4, "Stajyer Mühendis", new DateTime(2017, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, null, "Geta Grup Asansör", null, new DateTime(2017, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Asansör", false, 5, "Stajyer Mühendis", new DateTime(2017, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Level", "Name", "Order" },
                values: new object[,]
                {
                    { 1, "B2", "İngilizce", 1 },
                    { 2, "C2", "Türkçe", 2 }
                });

            migrationBuilder.InsertData(
                table: "PersonalInfos",
                columns: new[] { "Id", "About", "Address", "AvatarPath", "DrivingLicenseClass", "Email", "FullName", "Gender", "HasDrivingLicense", "MilitaryStatus", "Phone", "Title" },
                values: new object[] { 1, "Mekatronik mühendisliği mezunu, yazılım geliştirme tutkunu bir profesyonelim. .NET Core, C# ve modern web teknolojileri üzerine uzmanlaşmaktayım.", "Kavakpınar Mah. Zeytinli Cad. No:35/2 Pendik / İstanbul", null, "B", "veysel.mut@hotmail.com", "Veysel Mut", "Erkek", true, "Yapıldı", "+90 (531) 229 74 05", "Mekatronik Mühendisi / Yazılım Geliştirici" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Date", "Description", "Name", "Order", "Tasks", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASP.NET Core 8.0 MVC ile geliştirilmiş modern portföy ve CV sitesi", ".NET Core Tabanlı Bireysel Portföy Sitesi", 1, "Frontend tasarım\nBackend geliştirme\nVeritabanı yönetimi\nAdmin panel entegrasyonu", "https://github.com" },
                    { 2, new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kapsamlı e-ticaret platformu", "ASP.NET 6 Tabanlı E-Ticaret Sitesi", 2, "Ürün yönetimi\nSepet sistemi\nÖdeme entegrasyonu", null },
                    { 3, new DateTime(2022, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kurumsal personel takip ve yönetim sistemi", "Personel Yönetim Sistemi (PYS)", 3, "Personel kayıt\nİzin yönetimi\nRaporlama", null }
                });

            migrationBuilder.InsertData(
                table: "References",
                columns: new[] { "Id", "Company", "Email", "Name", "Order", "Phone", "Position" },
                values: new object[,]
                {
                    { 1, "Vakıf Katılım", "", "Nebi Erdal Akkabak", 1, "+90 (537) 363 18 15", "Bireysel Portföy Müdürü" },
                    { 2, "ODELLO OTOMOTİV", "m.fatihyilmaz@outlook.com", "Fatih Yılmaz", 2, "+90 (539) 947 47 59", "Kalıphane Mühendisi" },
                    { 3, "Softtech A.Ş.", "", "İsa Demir", 3, "+90 (535) 027 29 47", "Ürün Mimarı" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Level", "Name", "Order" },
                values: new object[,]
                {
                    { 1, 85, "C# / ASP.NET Core", 1 },
                    { 2, 80, "MS SQL Server", 2 },
                    { 3, 85, "Entity Framework Core", 3 },
                    { 4, 75, "HTML / CSS / JS", 4 },
                    { 5, 70, "Git / Docker", 5 }
                });

            migrationBuilder.InsertData(
                table: "SocialMedias",
                columns: new[] { "Id", "Github", "Instagram", "LinkedIn", "Twitter", "Youtube" },
                values: new object[] { 1, "https://github.com", null, "https://linkedin.com", null, null });

            migrationBuilder.InsertData(
                table: "WelcomeMessages",
                columns: new[] { "Id", "Description", "Subtitle", "Title" },
                values: new object[] { 1, ".NET Teknolojileri ve Modern Web Çözümleri üzerine uzmanlaşmış dijital portfolyoma hoş geldiniz.", "Mekatronik Mühendisi & Yazılım Geliştirici", "Merhaba, Ben Veysel Mut!" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "PersonalInfos");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "References");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "SocialMedias");

            migrationBuilder.DropTable(
                name: "WelcomeMessages");
        }
    }
}
