using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Entities;
using TripperAPI.Middleware.Exceptions;
using TripperAPI.Models;

namespace TripperAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly DatabaseContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger<AccountService> _logger;
        private readonly AuthConfiguration _authConfiguration;
        private readonly IEmailService _emailService;

        public AccountService(DatabaseContext context, IPasswordHasher<User> passwordHasher, ILogger<AccountService> logger, AuthConfiguration authConfiguration, IEmailService emailService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _logger = logger;
            _authConfiguration = authConfiguration;
            _emailService = emailService;
        }

        public string GenerateJwt(LoginDto dto)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);

            if(user is null)
            {
                throw new BadRequest("Invalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequest("Invalid username or password");
            }

            if(user.VerificationDate == null)
            {
                throw new BadRequest("Account not activated.");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")),
            };

            if(!string.IsNullOrEmpty(user.Nationality))
            {
                claims.Add(new Claim(ClaimTypes.Name, user.Nationality));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfiguration.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authConfiguration.JwtExpireDays);

            var token = new JwtSecurityToken(_authConfiguration.JwtIssuer,
                _authConfiguration.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public async Task RegisterUser(RegisterNewUserDto dto)
        {
            var user = new User
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId,
                VerificationToken = GenerateRandomToken()
            };

            var passwordhash = _passwordHasher.HashPassword(user, dto.Password);
            user.PasswordHash = passwordhash;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            await _emailService.SendEmail(user.Email, user.VerificationToken);

            _logger.LogInformation($"User with email: {user.Email} successfully registered");

        }

        public async Task ActivateAccount(string token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);

            if(user == null)
            {
                throw new BadRequest("Invalid Token");
            }

            user.VerificationDate = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        private string GenerateRandomToken()
        {
            var token = new byte[64];

            using(var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(token);
            }

            return Convert.ToHexString(token); 
        }
    }
}
