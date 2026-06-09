using kursovayaApi.Models;
using kursovayaApi.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;

namespace kursovayaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly KursovayaContext db;
        private readonly UsersService service;
        private readonly int workTimeMinutes = 20;

        public AccountController(KursovayaContext db)
        {
            this.db = db;
            service =new UsersService(db);
        }
        [Authorize]
        [HttpGet("info")]
        public IActionResult GetCurrentUserInfo()
        {
            string userName = HttpContext.User.Identity.Name;
            var user = db.Users.FirstOrDefault(p => p.Login == userName);
            if (user != null) return Ok(user);
            return NotFound();
        }
        [Authorize]
        [HttpGet("WorkTime")]
        public int GetWorkTimeInfo()
        {
            return workTimeMinutes;
        }
        [HttpPost("token")]
        public IActionResult GetToken(UserLogin user)
        {
            var identity = service.GetIdentity(user.Login, user.Password);
            if (identity != null)
            {   
                var now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE, claims: identity.Claims, notBefore: now,
                    expires: now.Add(TimeSpan.FromMinutes(workTimeMinutes)),
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(AuthOptions.GetSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var responce = new
                {
                    access_token = encodedJwt,
                    username = identity.Name
                };
                return Ok(responce);
            }
            return NotFound();
        }
    }
}
