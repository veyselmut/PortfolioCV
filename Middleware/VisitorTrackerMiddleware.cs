using Microsoft.AspNetCore.Http;
using PortfolioCV.Data;
using PortfolioCV.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace PortfolioCV.Middleware
{
    public class VisitorTrackerMiddleware
    {
        private readonly RequestDelegate _next;

        public VisitorTrackerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Sadece GET isteklerini ve Public sayfaları takip et
            var path = context.Request.Path.Value?.ToLower() ?? "/";
            
            // Admin paneli, API, static dosyalar ve favicon'u yoksay
            if (!path.StartsWith("/api") && 
                !path.StartsWith("/admin") && 
                !path.Contains(".") && // Dosya uzantısı varsa yoksay (css, js, png vb)
                context.Request.Method == "GET")
            {
                // Scoped service (DbContext) çağırmak için
                using (var scope = context.RequestServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    // Check X-Forwarded-For first (behind proxy/load balancer)
                    var ipAddress = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                    
                    if (string.IsNullOrEmpty(ipAddress))
                    {
                        ipAddress = context.Connection.RemoteIpAddress?.ToString();
                    }

                    // Normalize IPv6 Loopback
                    if (ipAddress == "::1") ipAddress = "127.0.0.1";
                    var userAgent = context.Request.Headers["User-Agent"].ToString();

                    // Basit bir Debounce: Aynı IP aynı gün içinde aynı sayfayı gezerse tekrar yazma
                    // Performans için bu kontrolü MemoryCache ile yapmak daha iyi olurdu ama şimdilik DB yeterli.
                    // Son 1 saat içinde kayıt var mı?
                    var lastVisit = await dbContext.Visitors
                        .Where(v => v.IpAddress == ipAddress && v.Path == path && v.VisitDate > DateTime.UtcNow.AddHours(-1))
                        .FirstOrDefaultAsync();

                    if (lastVisit == null)
                    {
                        var visitor = new Visitor
                        {
                            IpAddress = ipAddress ?? "Unknown",
                            UserAgent = userAgent,
                            Path = path,
                            VisitDate = DateTime.UtcNow
                        };

                        dbContext.Visitors.Add(visitor);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }

            await _next(context);
        }
    }
}
