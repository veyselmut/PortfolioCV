namespace PortfolioCV.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } // Proje açıklaması
    public string? Tasks { get; set; } // Görevler (virgülle ayrılmış veya satır satır)
    public DateTime Date { get; set; }
    public string? Url { get; set; }
    public int Order { get; set; }
}
