using LibraryAPI.Domain.Entities.Auth;
using LibraryAPI.Domain.Interfaces.IServices;
using LibraryAPI.Infrastructure.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace LibraryAPI.BL.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings emailSettings;
        private readonly ILogger<EmailService> logger;

        public EmailService(IOptions<EmailSettings> settings,
            ILogger<EmailService> logger)
        {
            this.emailSettings = settings.Value;
            this.logger = logger;
        }

        public void Send(User account, string subject, string? from = null)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? emailSettings.EmailFrom));
            email.To.Add(MailboxAddress.Parse(account.Email));
            email.Subject = subject;
            email.Body = new TextPart { Text = $"Your verify email token to verify email = {account.VerificationToken}" };

            using var smtp = new SmtpClient();

            smtp.CheckCertificateRevocation = false;

            smtp.Connect(emailSettings.SmtpHost, emailSettings.SmtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailSettings.SmtpUser, emailSettings.SmtpPass);
            smtp.Send(email);

            logger.LogInformation("Email has been sent");

            smtp.Disconnect(true);
        }
    }
}