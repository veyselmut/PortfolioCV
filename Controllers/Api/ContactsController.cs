using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PortfolioCV.Data;
using PortfolioCV.Models;

namespace PortfolioCV.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
public class ContactsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ContactsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Contact>>> GetAll()
    {
        var items = await _context.Contacts.OrderByDescending(c => c.CreatedAt).ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Contact>> GetById(int id)
    {
        var item = await _context.Contacts.FindAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPut("{id}/read")]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var item = await _context.Contacts.FindAsync(id);
        if (item == null) return NotFound();
        
        item.IsRead = true;
        await _context.SaveChangesAsync();
        return Ok(item);
    }

    [HttpDelete("{id}")]
    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Contacts.FindAsync(id);
        if (item == null) return NotFound();
        
        _context.Contacts.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpPut("{id}")]
    [HttpPost("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] System.Text.Json.JsonElement body)
    {
        var item = await _context.Contacts.FindAsync(id);
        if (item == null) return NotFound();

        // Handle partial update for IsRead
        if (body.TryGetProperty("isRead", out var isReadProp))
        {
            item.IsRead = isReadProp.GetBoolean();
        }

        await _context.SaveChangesAsync();
        return Ok(item);
    }
}

