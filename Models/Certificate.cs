namespace PortfolioCV.Models;

public class Certificate
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string? Description { get; set; }
    public int Order { get; set; }
}
