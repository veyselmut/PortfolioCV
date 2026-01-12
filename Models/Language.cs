namespace PortfolioCV.Models;

public class Language
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty; // A1, A2, B1, B2, C1, C2
    public int Order { get; set; }
}
