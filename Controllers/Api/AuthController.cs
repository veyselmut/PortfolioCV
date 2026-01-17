using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PortfolioCV.Data;
using PortfolioCV.Models;

using System.Text.Json.Serialization;

namespace PortfolioCV.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public class LoginRequest 
    {
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;
        
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
    }

    public record LoginResponse(string Token, string Username, DateTime Expiration);
    public record UserInfo(string Username, string Role);

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        Console.WriteLine($"[AUTH] Login attempt for: '{request.Username}'");
        
        var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == request.Username);

        if (admin == null)
        {
             Console.WriteLine($"[AUTH] User not found: '{request.Username}'");
             return Unauthorized(new { message = $"User not found. Input: '{request.Username}'" });
        }

        // Temporary Bypass for Debugging if needed. Currently keeping check active but verbose.
        if (!PortfolioCV.Helpers.SecurityHelper.VerifyPassword(admin.Password, request.Password))
        {
             Console.WriteLine("[AUTH] Password mismatch.");
             return Unauthorized(new { message = "Password mismatch" });
        }

        var token = GenerateJwtToken(admin);
        var expiration = DateTime.UtcNow.AddHours(24);

        return Ok(new LoginResponse(token, admin.Username, expiration));
    }

    [HttpGet("me")]
    [Authorize]
    public ActionResult<UserInfo> GetCurrentUser()
    {
        var username = User.FindFirst(ClaimTypes.Name)?.Value ?? "";
        return Ok(new UserInfo(username, "Admin"));
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        // JWT is stateless, so we just return success
        // Client should remove the token
        return Ok(new { message = "Logged out successfully" });
    }

    public class InitializeRequest
    {
        [JsonPropertyName("username")]
        public string Username { get; set; } = "";
        
        [JsonPropertyName("password")]
        public string Password { get; set; } = "";
        
        [JsonPropertyName("email")]
        public string Email { get; set; } = "";
    }

    [HttpPost("initialize")]
    public async Task<IActionResult> Initialize([FromBody] InitializeRequest request)
    {
        // Only allow initialization if no admin exists
        if (await _context.Admins.AnyAsync())
        {
            return BadRequest(new { message = "System already initialized. Admin user exists." });
        }

        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { message = "Username and password are required." });
        }

        var admin = new Admin
        {
            Username = request.Username,
            Password = PortfolioCV.Helpers.SecurityHelper.HashPassword(request.Password),
            Email = request.Email ?? ""
        };

        _context.Admins.Add(admin);
        await _context.SaveChangesAsync();

        Console.WriteLine($"[INIT] Admin '{request.Username}' created via /api/auth/initialize");

        return Ok(new { message = "Admin created successfully.", username = admin.Username });
    }

    [HttpGet("status")]
    public async Task<IActionResult> GetStatus()
    {
        var hasAdmin = await _context.Admins.AnyAsync();
        return Ok(new { initialized = hasAdmin });
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

