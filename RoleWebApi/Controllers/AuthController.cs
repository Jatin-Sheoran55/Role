using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Auth_Serv;
using Application.Roles.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RoleWebApi.Services;

namespace RoleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthApplication _authApplication;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthController(IAuthApplication authApplication, IConfiguration configuration, IEmailService emailService)
        {
            _authApplication = authApplication;
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpPost("Send-mail")]

        public async Task<IActionResult> SendMail(string to)
        {
            try
            {
                var subject = "Jatin Sheoran is Here";
                var body = "How are you   <b>Jatin Sheoran<b>";

                await _emailService.SendEmailAsync(to, subject, body);
                return Ok("Mail Sended");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Register")]

        public async Task<IActionResult> Register(AuthDto dto)
        {
            try
            {
                var response = await _authApplication.RegisterAsync(dto);
                return Ok(new { message = "User Registered successfully", data = response });
            }
            catch (Exception ex)
            {
                return BadRequest(new {error = ex.Message});
            }
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Auth(LoginDto dto)
        {
            try
            {
                var response = await _authApplication.LoginAsync(dto);

                var jwtSetting = _configuration.GetSection("jwt");
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting["key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                        new  Claim(JwtRegisteredClaimNames.Sub , response.UserName ),
                        new  Claim(ClaimTypes.Role, "Admin"),
                       // new  Claim(JwtRegisteredClaimNames.Jti ,"2232323232"),
                        new Claim("other","other")
                };

                var token = new JwtSecurityToken(
                            issuer: jwtSetting["Issuer"],
                            audience: jwtSetting["Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSetting["ExpiryInMinutes"])),
                            signingCredentials: creds);

                response.Token = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { message = "Login successful", data = response });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }
    }
}
