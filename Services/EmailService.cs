using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace PortfolioCV.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            try 
            {
                var senderEmail = _config["EmailSettings:SenderEmail"];
                var senderName = _config["EmailSettings:SenderName"];
                var password = _config["EmailSettings:Password"];
                var host = _config["EmailSettings:SmtpServer"];
                var port = int.Parse(_config["EmailSettings:SmtpPort"] ?? "587");

                // If password is default dummy value, skip real sending and just log.
                if (password == "YOUR_APP_PASSWORD_HERE" || string.IsNullOrEmpty(password))
                {
                    Console.WriteLine($"[Email Mock] To: {toEmail}, Subject: {subject}");
                    return; 
                }

                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(senderName, senderEmail));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = subject;

                var builder = new BodyBuilder { HtmlBody = message };
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                // For gmail: 587 + StartTls. For others it might vary.
                await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(senderEmail, password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                // Log error but don't crash the request
                Console.WriteLine($"[Email Error] {ex.Message}");
            }
        }
    }
}
