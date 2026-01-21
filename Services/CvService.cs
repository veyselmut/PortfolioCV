using Microsoft.EntityFrameworkCore;
using PortfolioCV.Data;
using PortfolioCV.Models;

namespace PortfolioCV.Services;

public class CvService
{
    private readonly AppDbContext _context;

    public CvService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CvViewModel> GetCvDataAsync()
    {
        var model = new CvViewModel();

        model.PersonalInfo = await _context.PersonalInfos.FirstOrDefaultAsync();
        model.Educations = await _context.Educations.OrderByDescending(x => x.StartDate).ToListAsync();
        model.Experiences = await _context.Experiences.OrderByDescending(x => x.StartDate).ToListAsync();
        model.Skills = await _context.Skills.OrderByDescending(x => x.Level).ToListAsync();
        model.Projects = await _context.Projects.OrderBy(x => x.Order).ToListAsync();
        model.Certificates = await _context.Certificates.OrderByDescending(x => x.Date).ToListAsync();
        model.Languages = await _context.Languages.OrderBy(x => x.Order).ToListAsync();
        model.SocialMedias = await _context.SocialMedias.ToListAsync();
        model.Services = await _context.Services.ToListAsync();
        model.References = await _context.References.ToListAsync();

        return model;
    }
}
