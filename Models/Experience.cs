using System.ComponentModel.DataAnnotations;

namespace PortfolioCV.Models;

public class Experience
{
    public int Id { get; set; }
    public string Position { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string? Industry { get; set; } // Firma Sektörü
    public string? Department { get; set; }
    public string? Address { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsCurrentlyWorking { get; set; } // Switch
    public int Order { get; set; }
}
