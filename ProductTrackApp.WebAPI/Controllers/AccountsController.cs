using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ProductTrackApp.Business.DTOs.Requests;
using ProductTrackApp.Business.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductTrackApp.WebAPI.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AccountsController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ValidateUserLoginRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.ValidateUserAsync(request);
                if (user != null)
                {
                    var jwtSettings = _configuration.GetSection("JwtSettings");
                    var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]);
                    var securityKey = new SymmetricSecurityKey(key);
                    var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    Claim[] claims = new Claim[]
                    {
                        new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.Role),
                    };

                    var token = new JwtSecurityToken(
                        issuer: jwtSettings["validIssuer"],
                        audience: jwtSettings["validAudience"],
                        claims: claims,
                        notBefore: DateTime.Now,
                        expires: DateTime.Now.AddMinutes(10),
                        signingCredentials: credential
                        );

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
                ModelState.AddModelError("", "Invalid credentials");
            }
            return BadRequest(ModelState);
        }
    }
}
