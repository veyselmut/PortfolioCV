using Microsoft.EntityFrameworkCore;
using PortfolioCV.Models;

namespace PortfolioCV.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<WelcomeMessage> WelcomeMessages { get; set; }
    public DbSet<PersonalInfo> PersonalInfos { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<CvReference> References { get; set; }
    public DbSet<SocialMedia> SocialMedias { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Visitor> Visitors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
