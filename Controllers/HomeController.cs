using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioCV.Data;
using PortfolioCV.Models;
using PortfolioCV.Services;



namespace PortfolioCV.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;

    public HomeController(AppDbContext context, IConfiguration configuration, IEmailService emailService)
    {
        _context = context;
        _configuration = configuration;
        _emailService = emailService;
    }

    [Route("")]
    [Route("Home")]
    [Route("Home/Index")]
    public async Task<IActionResult> Index()
    {
        var welcomeMessage = await _context.WelcomeMessages.FirstOrDefaultAsync();
        ViewBag.Services = await _context.Services.OrderBy(s => s.Order).ToListAsync();
        return View(welcomeMessage);
    }

    [Route("resume")]
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

    [Route("contact")]
    public IActionResult Contact()
    {
        return View();
    }

    [Route("contact")]
    [HttpPost]
    public async Task<IActionResult> Contact(Models.Contact contact)
    {
        if (ModelState.IsValid)
        {
            contact.CreatedAt = DateTime.Now;
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            // Email Notification
            try 
            {
                var adminEmail = _configuration["EmailSettings:SenderEmail"];
                if (!string.IsNullOrEmpty(adminEmail))
                {
                    await _emailService.SendEmailAsync(
                        adminEmail, 
                        $"[Portfolio] Yeni Mesaj: {contact.Subject}", 
                        $"<h3>Gönderen: {contact.Name} ({contact.Email})</h3><p>{contact.Message}</p>"
                    );
                }
            }
            catch(Exception ex) { Console.WriteLine($"Email send failed: {ex.Message}"); }

            TempData["Success"] = "Mesajınız başarıyla gönderildi!";
            return RedirectToAction("Contact");
        }
        return View(contact);
    }
}
