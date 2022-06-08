using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripperAPI.Services
{
    public interface IEmailService
    {
        Task SendAccountActivationEmail(string email, string token);
        Task SendPasswordResetEmail(string email, string token);

    }
}
