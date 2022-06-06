using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Models;

namespace TripperAPI.Services
{
    public interface IAccountService
    {
        Task RegisterUser(RegisterNewUserDto dto);
        string GenerateJwt(LoginDto dto);
        Task ActivateAccount(string token);
    }
}
