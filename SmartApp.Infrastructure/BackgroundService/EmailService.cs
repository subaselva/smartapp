using Microsoft.Extensions.Configuration;
using SendGrid.Helpers.Mail;
using SendGrid;
using SmartApp.Application.Service;
using SmartApp.Domain.ModelTemp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartApp.Infrastructure.BackgroundService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<Response> SendContactEmailAsync(ContactModel model, string restaurantEmail)
        {
            var apiKey = _config["SendGrid:SENDGRID_API_KEY"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(model.Email, model.Name);
            var subject = $"New Contact Message: {model.Subject}";
            var to = new EmailAddress(restaurantEmail);

            var plainTextContent = model.Message;
            var htmlContent = $"<p><strong>From:</strong> {model.Name} ({model.Email})</p><p>{model.Message}</p>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            return await client.SendEmailAsync(msg);
        }

    }
}
