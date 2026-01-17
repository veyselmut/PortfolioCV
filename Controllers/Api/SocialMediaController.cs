using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PortfolioCV.Data;
using PortfolioCV.Models;

namespace PortfolioCV.Controllers.Api;

[ApiController]
[Route("api/socialMedia")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
public class SocialMediaController : ControllerBase
{
    private readonly AppDbContext _context;

    public SocialMediaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<SocialMedia>> Get()
    {
        var item = await _context.SocialMedias.FirstOrDefaultAsync();
        if (item == null)
        {
            item = new SocialMedia();
            _context.SocialMedias.Add(item);
            await _context.SaveChangesAsync();
        }
        return Ok(item);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SocialMedia>> GetById(int id)
    {
        var item = await _context.SocialMedias.FirstOrDefaultAsync();
        if (item == null)
        {
            item = new SocialMedia();
            _context.SocialMedias.Add(item);
            await _context.SaveChangesAsync();
        }
        return Ok(item);
    }

    [HttpPut("{id}")]
    [HttpPost("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SocialMedia model)
    {
        var existing = await _context.SocialMedias.FirstOrDefaultAsync();
        if (existing == null)
        {
            _context.SocialMedias.Add(model);
        }
        else
        {
            model.Id = existing.Id;
            _context.Entry(existing).CurrentValues.SetValues(model);
        }
        await _context.SaveChangesAsync();
        return Ok(model);
    }
}
