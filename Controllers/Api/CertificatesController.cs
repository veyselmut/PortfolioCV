using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PortfolioCV.Data;
using PortfolioCV.Models;

namespace PortfolioCV.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
public class CertificatesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CertificatesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Certificate>>> GetAll()
    {
        var items = await _context.Certificates.OrderBy(e => e.Order).ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Certificate>> GetById(int id)
    {
        var item = await _context.Certificates.FindAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Certificate>> Create([FromBody] Certificate model)
    {
        _context.Certificates.Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
    }

    [HttpPut("{id}")]
    [HttpPost("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Certificate model)
    {
        if (id != model.Id) return BadRequest();
        
        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Ok(model);
    }

    [HttpDelete("{id}")]
    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Certificates.FindAsync(id);
        if (item == null) return NotFound();
        
        _context.Certificates.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
