using System;

namespace PortfolioCV.Models
{
    public class Visitor
    {
        public int Id { get; set; }
        public string IpAddress { get; set; } = string.Empty;
        public string UserAgent { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty; // Ziyaret edilen sayfa (/Home, /Details/1 vb)
        public DateTime VisitDate { get; set; } = DateTime.UtcNow;
    }
}
