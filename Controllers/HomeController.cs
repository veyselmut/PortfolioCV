using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioCV.Data;
using PortfolioCV.Models;

using System.IO;

namespace PortfolioCV.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public HomeController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var errFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "startup_error.txt");
            if (System.IO.File.Exists(errFile))
            {
                ViewBag.StartupError = System.IO.File.ReadAllText(errFile);
            }
            
            var welcomeMessage = await _context.WelcomeMessages.FirstOrDefaultAsync();
            return View(welcomeMessage);
        }
        catch (Exception ex)
        {
            var conn = _configuration.GetConnectionString("DefaultConnection");
            // Mask password
            if(!string.IsNullOrEmpty(conn)) {
                var parts = conn.Split(';');
                for(int i=0;i<parts.Length;i++) {
                    if(parts[i].Trim().StartsWith("Password", StringComparison.OrdinalIgnoreCase) || parts[i].Trim().StartsWith("Pwd", StringComparison.OrdinalIgnoreCase)) {
                        parts[i] = "Password=***";
                    }
                }
                conn = string.Join(";", parts);
            }
            
            ViewBag.DbError = ex.Message;
            ViewBag.FullEx = ex.ToString();
            ViewBag.ConnString = conn;
            return View((PortfolioCV.Models.WelcomeMessage)null);
        }
    }

    public async Task<IActionResult> CV()
    {
        ViewBag.PersonalInfo = await _context.PersonalInfos.FirstOrDefaultAsync();
        ViewBag.Educations = await _context.Educations.OrderBy(e => e.Order).ToListAsync();
        ViewBag.Experiences = await _context.Experiences.OrderBy(e => e.Order).ToListAsync();
        ViewBag.Skills = await _context.Skills.OrderBy(s => s.Order).ToListAsync();
        ViewBag.Projects = await _context.Projects.OrderBy(p => p.Order).ToListAsync();
        ViewBag.Certificates = await _context.Certificates.OrderBy(c => c.Order).ToListAsync();
        ViewBag.Languages = await _context.Languages.OrderBy(l => l.Order).ToListAsync();
        ViewBag.References = await _context.References.OrderBy(r => r.Order).ToListAsync();
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Contact(Models.Contact contact)
    {
        if (ModelState.IsValid)
        {
            contact.CreatedAt = DateTime.Now;
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Mesajınız başarıyla gönderildi!";
            return RedirectToAction("Contact");
        }
        return View(contact);
    }
}
