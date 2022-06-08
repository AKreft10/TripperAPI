using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Models;
using TripperAPI.Services;

namespace TripperAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterNewUser([FromBody] RegisterNewUserDto dto)
        {
            await _service.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginDto dto)
        {
            string token = _service.GenerateJwt(dto);
            return Ok(token);
        }

        [HttpPost("activate")]
        public async Task<ActionResult> ActivateAccount(string token)
        {
            await _service.ActivateAccount(token);
            return Ok("User activated successfully!");
        }

        [HttpGet("forget-password")]
        public async Task<ActionResult> ForgetPassword(string email)
        {
            await _service.ForgetPassword(email);
            return Ok("Email with password reset has been send!");
        }

        [HttpGet("reset-password")]
        public ActionResult ResetPassword(string token)
        {
            return Ok("Enter new password");
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(string token, [FromBody]ResetPasswordDto dto)
        {
            await _service.ResetPassword(token, dto);
            return Ok("Your password has been changed!");
        }
        
    }
}
