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
        public ActionResult RegisterNewUser([FromBody] RegisterNewUserDto dto)
        {
            _service.RegisterUser(dto);
            _emailService.SendEmail(dto.Email);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginDto dto)
        {
            string token = _service.GenerateJwt(dto);
            return Ok(token);
        }
    }
}
