using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PortfolioCV.Data;
using PortfolioCV.Models;

namespace PortfolioCV.Controllers.Api;

[ApiController]
[Route("api/welcomeMessage")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
public class WelcomeMessageController : ControllerBase
{
    private readonly AppDbContext _context;

    public WelcomeMessageController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<WelcomeMessage>> Get()
    {
        var item = await _context.WelcomeMessages.FirstOrDefaultAsync();
        if (item == null)
        {
            item = new WelcomeMessage
            {
                Title = "Welcome",
                Subtitle = "Your Subtitle Here",
                Description = "Your Description Here"
            };
            _context.WelcomeMessages.Add(item);
            await _context.SaveChangesAsync();
        }
        return Ok(item);
    }

    // For Refine useForm with id: 1
    [HttpGet("{id}")]
    public async Task<ActionResult<WelcomeMessage>> GetById(int id)
    {
        var item = await _context.WelcomeMessages.FirstOrDefaultAsync();
        if (item == null)
        {
            item = new WelcomeMessage
            {
                Title = "Welcome",
                Subtitle = "Your Subtitle Here",
                Description = "Your Description Here"
            };
            _context.WelcomeMessages.Add(item);
            await _context.SaveChangesAsync();
        }
        return Ok(item);
    }

    [HttpPut("{id}")]
    [HttpPost("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] WelcomeMessage model)
    {
        var existing = await _context.WelcomeMessages.FirstOrDefaultAsync();
        if (existing == null)
        {
            _context.WelcomeMessages.Add(model);
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

