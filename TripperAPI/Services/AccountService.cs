using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Entities;
using TripperAPI.Models;

namespace TripperAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly DatabaseContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger<AccountService> _logger;

        public AccountService(DatabaseContext context, IPasswordHasher<User> passwordHasher, ILogger<AccountService> logger)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }
        public async Task RegisterUser(RegisterNewUserDto dto)
        {
            var user = new User
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId
            };

            var passwordhash = _passwordHasher.HashPassword(user, dto.Password);
            user.PasswordHash = passwordhash;


            _logger.LogInformation($"User with email: {user.Email} successfully registered");

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();


        }
    }
}
