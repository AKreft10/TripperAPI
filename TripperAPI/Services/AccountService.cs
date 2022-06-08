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

            await _emailService.SendAccountActivationEmail(user.Email, user.VerificationToken);

            _logger.LogInformation($"User with email: {user.Email} successfully registered.");

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

            _logger.LogInformation($"User with email: {user.Email} successfully activated his account.");
        }

        public async Task ForgetPassword(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if(user == null)
            {
                throw new BadRequest("Wrong email address.");
            }

            user.PasswordResetToken = GenerateRandomToken();
            user.PasswordResetTokenExpires = DateTime.Now.AddMinutes(30);

            await _context.SaveChangesAsync();

            await _emailService.SendPasswordResetEmail(email, user.PasswordResetToken);

            _logger.LogInformation($"Reset password process for user with email: {user.Email} has been started.");
        }

        private string GenerateRandomToken()
        {
            var token = new byte[64];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(token);
            }

            return Convert.ToHexString(token);
        }

        public async Task ResetPassword(string token, ResetPasswordDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(t => t.PasswordResetToken == token);
            
            if(user.PasswordResetTokenExpires<DateTime.Now)
            {
                user.PasswordResetTokenExpires = null;
                user.PasswordResetToken = null;
                throw new BadRequest("Invalid token");
            }

            var newPassword = _passwordHasher.HashPassword(user, dto.Password);
            user.PasswordHash = newPassword;

            user.PasswordResetTokenExpires = null;
            user.PasswordResetToken = null;

            await _context.SaveChangesAsync();

            _logger.LogInformation($"User with email: {user.Email} succesfully reset password");
        }
    }
}
