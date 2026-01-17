using Microsoft.EntityFrameworkCore;
using PortfolioCV.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

// Add API Controllers with JSON support
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Database - SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=(localdb)\\mssqllocaldb;Database=PortfolioCV;Trusted_Connection=True;";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// JWT Configuration
var jwtKey = builder.Configuration["Jwt:Key"] ?? "YourSuperSecretKeyForJWTAuthenticationMin32Chars!";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "PortfolioCV";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "PortfolioCVAdmin";

// Authentication - Both Cookie (for existing MVC) and JWT (for new React API)
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    // Legacy cookie auth for MVC if still needed for frontend, 
    // but admin is now handled by JWT in React.
    options.Cookie.Name = "PortfolioCV.Auth";
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Authorization policies
builder.Services.AddAuthorization(options =>
{
    // Default policy for API controllers - requires JWT
    options.AddPolicy("ApiPolicy", policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });
});

// CORS for React Admin
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactAdmin", policy =>
    {
        policy.WithOrigins(allowedOrigins)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStaticFiles();

// Bypass IIS Delete/Put blocking by allowing Method Override via Header X-HTTP-Method-Override
app.UseHttpMethodOverride();

app.UseRouting();

// Enable CORS
app.UseCors("ReactAdmin");

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

// Map API routes (JWT auth)
app.MapControllers();

// Map MVC routes (Cookie auth)
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
        Console.WriteLine($"[STARTUP] Using Connection String: {connectionString}");
        
        // Initialize Database
        context.Database.EnsureCreated();
        Console.WriteLine($"[STARTUP] Database initialized.");
        
        // Seed Contacts if empty
        if (!context.Contacts.Any())
        {
            context.Contacts.AddRange(
                new PortfolioCV.Models.Contact
                {
                    Name = "Ahmet Yılmaz",
                    Email = "ahmet@example.com",
                    Subject = "Proje Teklifi",
                    Message = "Merhaba, web sitenizdeki çalışmalarınızı çok beğendim. Bir e-ticaret projesi için görüşmek isterim.",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-2)
                },
                new PortfolioCV.Models.Contact
                {
                    Name = "Zeynep Demir",
                    Email = "zeynep@company.com",
                    Subject = "İş Görüşmesi",
                    Message = "Frontend Developer pozisyonu için CV'nizi inceledik. Müsait olduğunuzda görüşmek isteriz.",
                    IsRead = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                }
            );
            Console.WriteLine("[STARTUP] Sample contacts seeded.");
        }

        context.SaveChanges();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

app.Run();
// Rebuild trigger 1
