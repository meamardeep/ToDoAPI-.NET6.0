using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using POC.BusinessLogic.Interfaces;
using POC.DataModel;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using TVShows.Data;

namespace POCAPI.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserManagement _userManagement;
        public LoginController(IConfiguration configuration, IUserManagement userManagement)
        {
            _configuration = configuration;
            _userManagement = userManagement;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/login")]
        public ActionResult LogIn([FromBody] Login login)
        {
            if(login == null || string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
            {
                return Unauthorized();
            }
            UserModel user = _userManagement.GetUser(login);
            if(!(user.UserId > 0))
                return Unauthorized(login);

            var authSignningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var authClaims = new List<Claim>
                {
                    new Claim("UserName", login.UserName),
                    new Claim("UserId", user.UserId.ToString())
                };

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignningKey, SecurityAlgorithms.HmacSha256)
                );

            return Ok(new {userId = user.UserId, token = new JwtSecurityTokenHandler().WriteToken(token), expires = token.ValidTo });    
        }
    }
}
