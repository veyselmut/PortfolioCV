using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioCV.Data;
using PortfolioCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioCV.Controllers.Api
{
    [Route("api/analytics")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnalyticsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            try
            {
                // Option B: Page Popularity (Pie Chart)
                var popularPages = await _context.Visitors
                    .GroupBy(v => v.Path)
                    .Select(g => new { Type = g.Key, Value = g.Count() })
                    .OrderByDescending(x => x.Value)
                    .Take(6)
                    .ToListAsync();

                // Mock Data if empty
                if (!popularPages.Any())
                {
                    var mockDt = new List<object>
                    {
                        new { Type = "/Home/Index", Value = 45 },
                        new { Type = "/Home/Contact", Value = 25 },
                        new { Type = "/Home/Projects", Value = 15 },
                        new { Type = "/Home/Resume", Value = 10 },
                        new { Type = "/Home/About", Value = 5 }
                    };
                    return Ok(new { chart = mockDt, total = 100 });
                }

                return Ok(new { chart = popularPages, total = popularPages.Sum(x => x.Value) });
            }
            catch (Exception ex)
            {
                 Console.WriteLine($"[Analytics Error] {ex.Message}");
                 return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("log")]
        public async Task<IActionResult> GetVisitorLog()
        {
            try
            {
                // Get last 50 visitors
                var visitors = await _context.Visitors
                    .OrderByDescending(v => v.VisitDate)
                    .Take(50)
                    .ToListAsync();
                
                // If empty, generate some mock data for display
                if (!visitors.Any())
                {
                     var mockList = new List<object>();
                     var rnd = new Random();
                     for(int i=0; i<10; i++)
                     {
                         mockList.Add(new {
                             Id = i,
                             IpAddress = $"192.168.1.{rnd.Next(10,99)}",
                             Path = i % 2 == 0 ? "/Home/Index" : "/Home/Contact",
                             UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64)",
                             VisitDate = DateTime.UtcNow.AddMinutes(-i * 15)
                         });
                     }
                     return Ok(mockList);
                }

                return Ok(visitors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
