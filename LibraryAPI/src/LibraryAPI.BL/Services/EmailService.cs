using LibraryAPI.Domain.Entities.Auth;
using LibraryAPI.Domain.Interfaces.IService;
using LibraryAPI.Infrastructure.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace LibraryAPI.BL.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _emailSettings = settings.Value;
        }

        public void Send(User account, string subject, string? from = null)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? _emailSettings.EmailFrom));
            email.To.Add(MailboxAddress.Parse(account.Email));
            email.Subject = subject;
            email.Body = new TextPart { Text = $"Your verify email token to verify email = {account.VerificationToken}" };

            using var smtp = new SmtpClient();

            smtp.CheckCertificateRevocation = false;

            smtp.Connect(_emailSettings.SmtpHost, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.SmtpUser, _emailSettings.SmtpPass);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}