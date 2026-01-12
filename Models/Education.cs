using System.ComponentModel.DataAnnotations;

namespace PortfolioCV.Models;

public class Education
{
    public int Id { get; set; }
    public string EducationType { get; set; } = string.Empty; // İlköğretim, Lise, Lisans, vb.
    public string? TeachingType { get; set; } // Açık Öğretim, İkinci, vb.
    public string School { get; set; } = string.Empty; // Üniversite veya Lise Adı
    public string? Department { get; set; } // Bölüm
    public string? Faculty { get; set; } // Fakülte
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsContinuing { get; set; } // Switch: Mezun/Devam
    public string? GradeSystem { get; set; } // 4, 5, 10, 100
    public double? Grade { get; set; }
    public int Order { get; set; }
}
