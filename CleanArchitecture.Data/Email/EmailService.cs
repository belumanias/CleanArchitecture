using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using SendWithBrevo;

namespace CleanArchitecture.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettings { get; }
        public ILogger<EmailService> _log { get; }

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> log)
        {
            _emailSettings = emailSettings.Value;
            _log = log;
        }

        public async Task<bool> SendEmailAsync(Application.Models.Email email)
        {
            BrevoClient client = new BrevoClient(_emailSettings.ApiKey);
            var subject = email.Subject;
            var to = new List<Recipient> { new Recipient("", email.To )};
            var emailBody = email.Body;
            var from = new Sender(_emailSettings.FromName, _emailSettings.FromAddress);
            var sendEmail = await client.SendAsync(from, to, subject, emailBody, false);
            if (!sendEmail)
            {
                _log.LogError("No pudo enviarse el email");
            }
            return sendEmail;
        }
    }
}
