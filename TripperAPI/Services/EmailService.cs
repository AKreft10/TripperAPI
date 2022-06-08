using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.SendGrid;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Models;

namespace TripperAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmailFactory _fluentEmail;
        private readonly IConfiguration _configuration;

        public EmailService(IFluentEmailFactory fluentEmail, IConfiguration configuration)
        {
            _fluentEmail = fluentEmail;
            _configuration = configuration;
        }
        public async Task SendAccountActivationEmail(string email, string token)
        {
            string activateLink = await Task.FromResult(GenerateLinkWithToken(token));

            var emailToSend = _fluentEmail
            .Create()
            .To(email)
            .Subject($"Activate your Tripper Account!")
            .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/wwwroot/EmailTemplates/RegisterNewUserEmail.cshtml", new { activateLink });

            var result = await emailToSend.SendAsync();  
        }

        public async Task SendPasswordResetEmail(string email, string token)
        {
            string resetLink = await Task.FromResult(GeneratePasswordResetLinkWithToken(token));

            var emailToSend = _fluentEmail
                .Create()
                .To(email)
                .Subject($"Tripper password reset")
                .Body(token);

            await emailToSend.SendAsync();
        }

        private string GenerateLinkWithToken(string token) => $"https://localhost:5001/api/account/activate?token={token}";
        private string GeneratePasswordResetLinkWithToken(string token) => $"https://localhost:5001/api/account/password-reset?token={token}";
    }
}
