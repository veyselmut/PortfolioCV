using Microsoft.EntityFrameworkCore;
using PortfolioCV.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// AUTO-FIX: Force ignore SSL certificate errors for specific hosting environments
if (connectionString != null && !connectionString.Contains("TrustServerCertificate", StringComparison.OrdinalIgnoreCase))
{
    connectionString += ";TrustServerCertificate=True;Encrypt=False";
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login";
        options.LogoutPath = "/Admin/Logout";
        options.AccessDeniedPath = "/Admin/Login";
    });

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Localization
var supportedCultures = new[] { "tr-TR", "en-US" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("tr-TR")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();

        // Admin Seed/Update
        var admin = context.Admins.FirstOrDefault();
        if (admin == null)
        {
            context.Admins.Add(new PortfolioCV.Models.Admin { Username = "praimkepa", Password = PortfolioCV.Helpers.SecurityHelper.HashPassword("3408v+-0"), Email = "admin@example.com" });
        }
        else
        {
            // Update password only if username matches (or force update for security fix)
            if (admin.Username == "praimkepa" && !admin.Password.Contains(".")) 
            {
                 admin.Password = PortfolioCV.Helpers.SecurityHelper.HashPassword("3408v+-0");
            }
        }
        
        // Cleanup old admin if exists
        var oldAdmin = context.Admins.FirstOrDefault(a => a.Username == "admin");
        if(oldAdmin != null)
        {
            context.Admins.Remove(oldAdmin);
        }

        context.SaveChanges();
    }
    catch (Exception ex)
    {
        Console.WriteLine("DB INIT ERROR: " + ex.Message);
        try {
            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "wwwroot", "startup_error.txt"), ex.ToString());
        } catch { /* Ignore file write error */ }
    }
}

app.Run();
