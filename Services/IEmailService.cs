using System.Threading.Tasks;

namespace PortfolioCV.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
