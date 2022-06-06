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
        private readonly IEmailService _emailService;

        public AccountController(IAccountService service, IEmailService emailService)
        {
            _service = service;
            _emailService = emailService;
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

        
    }
}
