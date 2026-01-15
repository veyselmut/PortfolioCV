namespace PortfolioCV.Models;

public class Service
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = "fas fa-code"; // Font Awesome icon class
    public int Order { get; set; }
}
