using System.ComponentModel.DataAnnotations;

namespace PortfolioCV.Models;

public class PersonalInfo
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty; // Kadın, Erkek
    public bool HasDrivingLicense { get; set; }
    public string? DrivingLicenseClass { get; set; }
    public string MilitaryStatus { get; set; } = string.Empty; // Muaf, Tecilli, Yapıldı, Yapılmadı
    public string? AvatarPath { get; set; }
    public string About { get; set; } = string.Empty;
}
