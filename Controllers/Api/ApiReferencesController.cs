using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PortfolioCV.Data;
using PortfolioCV.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioCV.Controllers.Api;

[ApiController]
[Route("api/references")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
public class ApiReferencesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ApiReferencesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<CvReference>>> GetAll()
    {
        var items = await _context.References.OrderBy(e => e.Order).ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CvReference>> GetById(int id)
    {
        var item = await _context.References.FindAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<CvReference>> Create([FromBody] CvReference model)
    {
        _context.References.Add(model);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
    }

    [HttpPut("{id}")]
    [HttpPost("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CvReference model)
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
        var item = await _context.References.FindAsync(id);
        if (item == null) return NotFound();
        
        _context.References.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
