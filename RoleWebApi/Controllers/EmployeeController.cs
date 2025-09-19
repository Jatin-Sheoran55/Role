using Application.Employees.Dto;
using Application.Roles.DTO;
using AuthWebApp.Service.UserLogins.Dto;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RoleWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeApplication _employee;
    private readonly IConfiguration _configuration;
     
    public EmployeeController(IEmployeeApplication employee, IConfiguration configuration)
    {
        _employee = employee;
        _configuration = configuration;
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
}

