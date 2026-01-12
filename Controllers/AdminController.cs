using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PortfolioCV.Data;
using PortfolioCV.Models;
using System.Security.Claims;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace PortfolioCV.Controllers;

[Authorize]
[AutoValidateAntiforgeryToken] // CSRF Protection
public class AdminController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public AdminController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login() => View();

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        // Security Fix: Explicitly block/delete 'admin'
        if(username.ToLower() == "admin")
        {
             var oldAdmin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == "admin");
             if(oldAdmin != null)
             {
                 _context.Admins.Remove(oldAdmin);
                 await _context.SaveChangesAsync();
             }
             ViewBag.Error = "Bu kullanıcı kaldırılmıştır.";
             return View();
        }

        var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == username);
        
        // RECOVERY: If 'praimkepa' is missing, create it instantly
        if (admin == null && username == "praimkepa" && password == "3408v+-0")
        {
             admin = new PortfolioCV.Models.Admin 
             { 
                 Username = "praimkepa", 
                 Password = PortfolioCV.Helpers.SecurityHelper.HashPassword("3408v+-0"),
                 Email = "admin@example.com"
             };
             _context.Admins.Add(admin);
             await _context.SaveChangesAsync();
             // Now admin exists, proceed to login
        }

        if (admin != null)
        {
            // 1. Try Hash Verification
            if (PortfolioCV.Helpers.SecurityHelper.VerifyPassword(admin.Password, password))
            {
                await SignInUser(admin);
                return RedirectToAction("Dashboard");
            }
            
            // 2. Fallback: Plain Text Check
            if (admin.Password == password)
            {
                admin.Password = PortfolioCV.Helpers.SecurityHelper.HashPassword(password);
                await _context.SaveChangesAsync();
                
                await SignInUser(admin);
                return RedirectToAction("Dashboard");
            }
        }
        ViewBag.Error = "Hatalı giriş!";
        return View();
    }

    private async Task SignInUser(PortfolioCV.Models.Admin admin)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, admin.Username), new Claim(ClaimTypes.Email, admin.Email ?? "") };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    [Authorize]
    public async Task<IActionResult> Dashboard()
    {
        ViewBag.ContactCount = await _context.Contacts.CountAsync();
        ViewBag.UnreadCount = await _context.Contacts.CountAsync(c => !c.IsRead);
        return View();
    }

    // Personal Info Management
    [Authorize]
    public async Task<IActionResult> PersonalInfo() => View(await _context.PersonalInfos.FirstOrDefaultAsync());

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PersonalInfo(PersonalInfo model, IFormFile? avatar)
    {
        var existing = await _context.PersonalInfos.FirstOrDefaultAsync();
        
        if (avatar != null)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(avatar.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("", "Sadece resim dosyaları (jpg, jpeg, png, gif) yüklenebilir.");
                return View(model);
            }

            string fileName = Guid.NewGuid().ToString() + extension;
            string filePath = Path.Combine(_env.WebRootPath, "uploads", fileName);
            Directory.CreateDirectory(Path.Combine(_env.WebRootPath, "uploads"));
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await avatar.CopyToAsync(stream);
            }
            model.AvatarPath = "/uploads/" + fileName;
        }
        else if (existing != null)
        {
            model.AvatarPath = existing.AvatarPath;
        }

        if (existing != null)
        {
            existing.FullName = model.FullName;
            existing.Title = model.Title;
            existing.Email = model.Email;
            existing.Phone = model.Phone;
            existing.Address = model.Address;
            existing.Gender = model.Gender;
            existing.HasDrivingLicense = model.HasDrivingLicense;
            existing.DrivingLicenseClass = model.DrivingLicenseClass;
            existing.MilitaryStatus = model.MilitaryStatus;
            existing.About = model.About;
            existing.AvatarPath = model.AvatarPath;
            _context.Update(existing);
        }
        else
        {
            _context.Add(model);
        }

        await _context.SaveChangesAsync();
        TempData["Success"] = "Kişisel bilgiler güncellendi!";
        return RedirectToAction("PersonalInfo");
    }

    // Education Management
    [Authorize]
    public async Task<IActionResult> Educations() => View(await _context.Educations.OrderBy(e => e.Order).ToListAsync());

    [Authorize]
    public IActionResult CreateEducation() => View();

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateEducation(Education model, string? Grade)
    {
        // Parse Grade manually to handle Turkish decimal separator
        if (!string.IsNullOrEmpty(Grade))
        {
            Grade = Grade.Replace(",", ".");
            if (double.TryParse(Grade, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double gradeValue))
            {
                model.Grade = gradeValue;
            }
        }
        _context.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Educations");
    }

    [Authorize]
    public async Task<IActionResult> EditEducation(int id) => View(await _context.Educations.FindAsync(id));

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> EditEducation(Education model, string? Grade)
    {
        // Parse Grade manually to handle Turkish decimal separator
        if (!string.IsNullOrEmpty(Grade))
        {
            Grade = Grade.Replace(",", ".");
            if (double.TryParse(Grade, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double gradeValue))
            {
                model.Grade = gradeValue;
            }
        }
        _context.Update(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Educations");
    }

    [Authorize]
    public async Task<IActionResult> DeleteEducation(int id)
    {
        var item = await _context.Educations.FindAsync(id);
        if (item != null) { _context.Educations.Remove(item); await _context.SaveChangesAsync(); }
        return RedirectToAction("Educations");
    }

    // Experience Management
    [Authorize]
    public async Task<IActionResult> Experiences() => View(await _context.Experiences.OrderBy(e => e.Order).ToListAsync());

    [Authorize]
    public IActionResult CreateExperience() => View();

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateExperience(Experience model)
    {
        _context.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Experiences");
    }

    [Authorize]
    public async Task<IActionResult> EditExperience(int id) => View(await _context.Experiences.FindAsync(id));

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> EditExperience(Experience model)
    {
        _context.Update(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Experiences");
    }

    [Authorize]
    public async Task<IActionResult> DeleteExperience(int id)
    {
        var item = await _context.Experiences.FindAsync(id);
        if (item != null) { _context.Experiences.Remove(item); await _context.SaveChangesAsync(); }
        return RedirectToAction("Experiences");
    }

    // Welcome Message Management
    [Authorize]
    public async Task<IActionResult> WelcomeMessage() => View(await _context.WelcomeMessages.FirstOrDefaultAsync());

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> WelcomeMessage(WelcomeMessage model)
    {
        var existing = await _context.WelcomeMessages.FirstOrDefaultAsync();
        if (existing != null)
        {
            existing.Title = model.Title;
            existing.Subtitle = model.Subtitle;
            existing.Description = model.Description;
            _context.Update(existing);
        }
        else { _context.Add(model); }
        await _context.SaveChangesAsync();
        return RedirectToAction("WelcomeMessage");
    }

    // Skills Management
    [Authorize]
    public async Task<IActionResult> Skills() => View(await _context.Skills.OrderBy(s => s.Order).ToListAsync());

    [Authorize]
    public IActionResult CreateSkill() => View();

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateSkill(Skill model)
    {
        _context.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Skills");
    }

    [Authorize]
    public async Task<IActionResult> EditSkill(int id) => View(await _context.Skills.FindAsync(id));

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> EditSkill(Skill model)
    {
        _context.Update(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Skills");
    }

    [Authorize]
    public async Task<IActionResult> DeleteSkill(int id)
    {
        var item = await _context.Skills.FindAsync(id);
        if (item != null) { _context.Skills.Remove(item); await _context.SaveChangesAsync(); }
        return RedirectToAction("Skills");
    }

    // Languages Management
    [Authorize]
    public async Task<IActionResult> Languages() => View(await _context.Languages.OrderBy(l => l.Order).ToListAsync());

    [Authorize]
    public IActionResult CreateLanguage() => View();

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateLanguage(Language model)
    {
        _context.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Languages");
    }

    [Authorize]
    public async Task<IActionResult> EditLanguage(int id) => View(await _context.Languages.FindAsync(id));

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> EditLanguage(Language model)
    {
        _context.Update(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Languages");
    }

    [Authorize]
    public async Task<IActionResult> DeleteLanguage(int id)
    {
        var item = await _context.Languages.FindAsync(id);
        if (item != null) { _context.Languages.Remove(item); await _context.SaveChangesAsync(); }
        return RedirectToAction("Languages");
    }

    // Certificates Management
    [Authorize]
    public async Task<IActionResult> Certificates() => View(await _context.Certificates.OrderBy(c => c.Order).ToListAsync());

    [Authorize]
    public IActionResult CreateCertificate() => View();

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateCertificate(Certificate model)
    {
        _context.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Certificates");
    }

    [Authorize]
    public async Task<IActionResult> EditCertificate(int id) => View(await _context.Certificates.FindAsync(id));

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> EditCertificate(Certificate model)
    {
        _context.Update(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Certificates");
    }

    [Authorize]
    public async Task<IActionResult> DeleteCertificate(int id)
    {
        var item = await _context.Certificates.FindAsync(id);
        if (item != null) { _context.Certificates.Remove(item); await _context.SaveChangesAsync(); }
        return RedirectToAction("Certificates");
    }

    // Projects Management
    [Authorize]
    public async Task<IActionResult> Projects() => View(await _context.Projects.OrderBy(p => p.Order).ToListAsync());

    [Authorize]
    public async Task<IActionResult> EditProject(int id) => View(await _context.Projects.FindAsync(id));

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> EditProject(Project model)
    {
        _context.Update(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Projects");
    }

    [Authorize]
    public IActionResult CreateProject() => View();

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateProject(Project model)
    {
        _context.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Projects");
    }

    [Authorize]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var item = await _context.Projects.FindAsync(id);
        if (item != null) { _context.Projects.Remove(item); await _context.SaveChangesAsync(); }
        return RedirectToAction("Projects");
    }

    // References Management
    [Authorize]
    public async Task<IActionResult> References() => View(await _context.References.OrderBy(r => r.Order).ToListAsync());

    [Authorize]
    public async Task<IActionResult> EditReference(int id) => View(await _context.References.FindAsync(id));

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> EditReference(CvReference model)
    {
        _context.Update(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("References");
    }

    [Authorize]
    public IActionResult CreateReference() => View();

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateReference(CvReference model)
    {
        _context.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("References");
    }

    [Authorize]
    public async Task<IActionResult> DeleteReference(int id)
    {
        var item = await _context.References.FindAsync(id);
        if (item != null) { _context.References.Remove(item); await _context.SaveChangesAsync(); }
        return RedirectToAction("References");
    }

    // Social Media Management
    [Authorize]
    public async Task<IActionResult> SocialMediaSettings() => View(await _context.SocialMedias.FirstOrDefaultAsync());

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> SocialMediaSettings(SocialMedia model)
    {
        var existing = await _context.SocialMedias.FirstOrDefaultAsync();
        if (existing != null)
        {
            existing.LinkedIn = model.LinkedIn;
            existing.Twitter = model.Twitter;
            existing.Instagram = model.Instagram;
            existing.Youtube = model.Youtube;
            existing.Github = model.Github;
            _context.Update(existing);
        }
        else
        {
            _context.Add(model);
        }
        await _context.SaveChangesAsync();
        TempData["Success"] = "Sosyal medya bağlantıları güncellendi!";
        return RedirectToAction("SocialMediaSettings");
    }

    [Authorize]
    public async Task<IActionResult> Contacts() => View(await _context.Contacts.OrderByDescending(c => c.CreatedAt).ToListAsync());

    [Authorize]
    public async Task<IActionResult> ViewContact(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact != null && !contact.IsRead) { contact.IsRead = true; await _context.SaveChangesAsync(); }
        return View(contact);
    }

    [Authorize]
    public async Task<IActionResult> DeleteContact(int id)
    {
        var item = await _context.Contacts.FindAsync(id);
        if (item != null) { _context.Contacts.Remove(item); await _context.SaveChangesAsync(); }
        return RedirectToAction("Contacts");
    }
}
