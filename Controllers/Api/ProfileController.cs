using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using PortfolioCV.Data;
using PortfolioCV.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using System.Text;

namespace PortfolioCV.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProfileController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public ProfileController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public record ProfileResponse(int Id, string Username, string Email, string? Token = null);
    public record ProfileUpdate(string Username, string Email, string CurrentPassword, string? NewPassword, string? ConfirmPassword);

    [HttpGet("me")]
    public async Task<ActionResult<ProfileResponse>> GetProfile()
    {
        var username = User.FindFirst(ClaimTypes.Name)?.Value;
        
        if (string.IsNullOrEmpty(username)) 
        {
            return Unauthorized();
        }

        var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == username);
        if (admin == null) 
        {
            return NotFound();
        }

        return Ok(new ProfileResponse(admin.Id, admin.Username, admin.Email));
    }

    [HttpPut("me")]
    public async Task<ActionResult<ProfileResponse>> UpdateProfile([FromBody] ProfileUpdate request)
    {
        var currentUsername = User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(currentUsername)) return Unauthorized();

        var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == currentUsername);
        if (admin == null) return NotFound();

        // 1. SECURITY GATE: Verify Current Password (MANDATORY)
        if (string.IsNullOrEmpty(request.CurrentPassword))
        {
            return BadRequest(new { message = "Güvenlik gereği, değişiklik yapmak için MEVCUT ŞİFRENİZİ girmelisiniz." });
        }

        if (!PortfolioCV.Helpers.SecurityHelper.VerifyPassword(admin.Password, request.CurrentPassword))
        {
            return BadRequest(new { message = "Mevcut şifreniz hatalı. Lütfen tekrar deneyin." });
        }

        // 2. CHECK: Username Uniqueness (if changed)
        bool usernameChanged = !string.Equals(request.Username, currentUsername, StringComparison.OrdinalIgnoreCase);
        if (usernameChanged)
        {
            if (await _context.Admins.AnyAsync(a => a.Username == request.Username))
            {
                return BadRequest(new { message = "Bu kullanıcı adı zaten başkası tarafından kullanılıyor." });
            }
        }

        // 3. UPDATE: Basic Info
        admin.Username = request.Username;
        admin.Email = request.Email;

        // 4. UPDATE: Password (OPTIONAL)
        if (!string.IsNullOrWhiteSpace(request.NewPassword))
        {
            admin.Password = PortfolioCV.Helpers.SecurityHelper.HashPassword(request.NewPassword);
        }

        try 
        {
            await _context.SaveChangesAsync();
            
            // 5. ISSUE NEW TOKEN if username changed
            string? newToken = null;
            if (usernameChanged)
            {
                newToken = GenerateJwtToken(admin);
            }
            
            return Ok(new ProfileResponse(admin.Id, admin.Username, admin.Email, newToken));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Veritabanı güncelleme hatası: " + ex.Message });
        }
    }
    
    private string GenerateJwtToken(Admin admin)
    {
        var jwtKey = _config["Jwt:Key"] ?? "YourSuperSecretKeyForJWTAuthenticationMin32Chars!";
        var jwtIssuer = _config["Jwt:Issuer"] ?? "PortfolioCV";
        var jwtAudience = _config["Jwt:Audience"] ?? "PortfolioCVAdmin";

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, admin.Username),
            new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

