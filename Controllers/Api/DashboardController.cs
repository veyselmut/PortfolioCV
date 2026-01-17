using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PortfolioCV.Data;
using PortfolioCV.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PortfolioCV.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class DashboardController : ControllerBase
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    public record DashboardStats(
        int ContactCount,
        int UnreadCount,
        int ProjectCount,
        int EducationCount,
        int ExperienceCount,
        int SkillCount,
        int CertificateCount,
        int ServiceCount
    );

    public record LatestMessage(
        int Id,
        string Name,
        string Email,
        string Subject,
        bool IsRead,
        DateTime CreatedAt
    );

    public record DashboardData(
        DashboardStats Stats,
        List<LatestMessage> LatestMessages
    );

    [HttpGet]
    public async Task<ActionResult<DashboardData>> GetDashboard()
    {
        var stats = new DashboardStats(
            ContactCount: await _context.Contacts.CountAsync(),
            UnreadCount: await _context.Contacts.CountAsync(c => !c.IsRead),
            ProjectCount: await _context.Projects.CountAsync(),
            EducationCount: await _context.Educations.CountAsync(),
            ExperienceCount: await _context.Experiences.CountAsync(),
            SkillCount: await _context.Skills.CountAsync(),
            CertificateCount: await _context.Certificates.CountAsync(),
            ServiceCount: await _context.Services.CountAsync()
        );

        var latestMessages = await _context.Contacts
            .OrderByDescending(c => c.CreatedAt)
            .Take(5)
            .Select(c => new LatestMessage(
                c.Id,
                c.Name,
                c.Email,
                c.Subject,
                c.IsRead,
                c.CreatedAt
            ))
            .ToListAsync();

        return Ok(new DashboardData(stats, latestMessages));
    }
}

