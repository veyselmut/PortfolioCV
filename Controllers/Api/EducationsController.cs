using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PortfolioCV.Data;
using PortfolioCV.Models;

namespace PortfolioCV.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
public class EducationsController : ControllerBase
{
    private readonly AppDbContext _context;

    public EducationsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Education>>> GetAll()
    {
        var items = await _context.Educations.OrderBy(e => e.Order).ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Education>> GetById(int id)
    {
        var item = await _context.Educations.FindAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Education>> Create([FromBody] Education model)
    {
        _context.Educations.Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
    }

    [HttpPut("{id}")]
    [HttpPost("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Education model)
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
        var item = await _context.Educations.FindAsync(id);
        if (item == null) return NotFound();
        
        _context.Educations.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

