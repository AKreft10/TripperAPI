using FluentEmail.Core;
using FluentEmail.Razor;
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
        public async Task SendEmail(string email)
        {
            var emailToSend = _fluentEmail
                .Create()
                .To(email)
                .Subject($"Activate your Tripper Account!")
                .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/wwwroot/EmailTemplates/RegisterNewUserEmail.cshtml", new {});


            await emailToSend.SendAsync();
        }
    }
}
