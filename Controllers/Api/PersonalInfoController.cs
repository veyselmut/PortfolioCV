using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PortfolioCV.Data;
using PortfolioCV.Models;

namespace PortfolioCV.Controllers.Api;

[ApiController]
[Route("api/personalInfo")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
public class PersonalInfoController : ControllerBase
{
    private readonly AppDbContext _context;

    public PersonalInfoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<PersonalInfo>> Get()
    {
        var item = await _context.PersonalInfos.FirstOrDefaultAsync();
        if (item == null)
        {
            item = new PersonalInfo();
            _context.PersonalInfos.Add(item);
            await _context.SaveChangesAsync();
        }
        return Ok(item);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonalInfo>> GetById(int id)
    {
        var item = await _context.PersonalInfos.FirstOrDefaultAsync();
        if (item == null)
        {
            item = new PersonalInfo();
            _context.PersonalInfos.Add(item);
            await _context.SaveChangesAsync();
        }
        return Ok(item);
    }

    [HttpPut("{id}")]
    [HttpPost("update/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PersonalInfo model)
    {
        var existing = await _context.PersonalInfos.FirstOrDefaultAsync();
        if (existing == null)
        {
            _context.PersonalInfos.Add(model);
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
