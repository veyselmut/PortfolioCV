using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioCV.Data;
using PortfolioCV.Models;

namespace PortfolioCV.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public SidebarViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var personalInfo = await _context.PersonalInfos.FirstOrDefaultAsync();
                var languages = await _context.Languages.OrderBy(l => l.Order).ToListAsync();
                var socialMedia = await _context.SocialMedias.FirstOrDefaultAsync();

                ViewBag.PersonalInfo = personalInfo;
                ViewBag.Languages = languages;
                ViewBag.SocialMedia = socialMedia;
            }
            catch
            {
                // Suppress error so Debug page can show the real error
                ViewBag.PersonalInfo = null;
                ViewBag.Languages = new List<PortfolioCV.Models.Language>();
                ViewBag.SocialMedia = null;
            }

            return View();
        }
    }
}
