using System.Collections.Generic;

namespace PortfolioCV.Models;

public class CvViewModel
{
    public PersonalInfo? PersonalInfo { get; set; }
    public List<Education> Educations { get; set; } = new();
    public List<Experience> Experiences { get; set; } = new();
    public List<Skill> Skills { get; set; } = new();
    public List<Project> Projects { get; set; } = new();
    public List<Certificate> Certificates { get; set; } = new();
    public List<Language> Languages { get; set; } = new();
    public List<SocialMedia> SocialMedias { get; set; } = new();
    public List<Service> Services { get; set; } = new();
    public List<CvReference> References { get; set; } = new();
}
