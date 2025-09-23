using Application.Employees.Dto;
using Application.Roles.DTO;
using AuthWebApp.Service.UserLogins.Dto;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RoleWebApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RoleWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeApplication _employee;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
     
    public EmployeeController(IEmployeeApplication employee, 
        IConfiguration configuration,
        IEmailService emailService)
    {
        _employee = employee;
        _configuration = configuration;
        _emailService = emailService;
     }

    [HttpPost]
    public async Task<IActionResult> Post(CreateEmployeeDto input)
    {
        try
        {

            var id = await _employee.CreateEmployee(input);
            return Ok(id);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> EmployeeLogin(LoginDto input)
    {
        try
        {
            var response = await _employee.LoginAsync(input);

            var jwtSetting = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting["key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                    new  Claim(JwtRegisteredClaimNames.Sub , response.Id.ToString() ),
                    new  Claim(ClaimTypes.Role, response.Role), 
                    new Claim("other","other")
                 };

            var token = new JwtSecurityToken(
               issuer: jwtSetting["Issuer"],
               audience: jwtSetting["Audience"],
               claims: claims,
               expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSetting["ExpiryInMinutes"])),
               signingCredentials: creds
      );

            response.Token = new JwtSecurityTokenHandler().WriteToken(token);



            return Ok(new { message = "Login successful", data = response });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { error = ex.Message });
        }   

    }

    [Authorize]
    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto input)
    {

        try
        { 

            var user = HttpContext.User;

            var userid = user.FindFirstValue(ClaimTypes.NameIdentifier)
                        ?? user.FindFirstValue("Sub");
                 
            await _employee.ChangePasswordAsync(Convert.ToInt32( userid),input);

            return Ok("Passsword Changed ");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }


    [HttpGet("forget-password/{emailId}")]
    public async Task<IActionResult> ForgetPassword(string emailId)
    {
        try
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            var code = await _employee.ForgetPasswordAsync(emailId, ipAddress);
             
            await SendResetPasswordEmail(emailId, code);
            
            return Ok("reset password link sended to your EmailId");

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }


    }



    [HttpPut("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto input)
    {
        try
        { 
            await _employee.ResetPassword(input);
            return Ok("reset successfully!");

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }


    }
    private async Task SendResetPasswordEmail(string email, string code)
    {
        var subject = "Reset Your Password - Your Website";

        // Suppose your website reset URL
        var resetUrl = $"https://localhost:7012/employee/reset-password/{code}";


        var body = $@"
    <html>
    <body style='font-family: Arial, sans-serif; font-size: 14px; color: #333;'>
        <p>Hi,</p>
        <p>We received a request to reset your password.</p>
        <p>
            Please click the link below to reset your password:
        </p>
        <p>
            <a href='{resetUrl}' style='background-color: #007bff; color: #fff; padding: 8px 12px; text-decoration: none; border-radius: 4px;'>Reset Password</a>
        </p>
        <p>If you did not request a password reset, you can safely ignore this email or consider changing your password for security purposes.</p>
        <p>Thank you,<br/>Your Website Team</p>
    </body>
    </html>";

      await   _emailService.SendEmailAsync(email, subject, body);
    }


}

