using Application.Employees;
using Application.Roles.DTO;
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

    public EmployeeController(IEmployeeApplication employee)
    {
        _employee = employee;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateEmployeeDto input)
    {
        try
        {

            var data = await _employee.CreateEmployee(input);
            return Ok( data.Id);

        } 
        catch( Exception ex)
        {
          return  BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> EmployeeLogin(CreateEmployeeDto input)
    {
        try
        {
            var response = await _userLoginService.LoginAsync(dto);

            var jwtSetting = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting["key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                    new  Claim(JwtRegisteredClaimNames.Sub , response.Id.ToString() ),
                    new  Claim(ClaimTypes.Role, "Admin"),
                   // new  Claim(JwtRegisteredClaimNames.Jti ,"2232323232"),
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

    [HttpGet]
    public async Task<List<EmployeeDto>> GetAll()
    {
        return await _employee.GetAllEmployees();
    }
    [HttpGet("{id}")]

    public async Task<EmployeeDto> Get(int id)
    {
        return await _employee.GetById(id);
    }

    [HttpPut("{id}")]
    public async Task Put(int id, CreateEmployeeDto input)
    {
        await _employee.GetById(id);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _employee.DeleteEmployee(id);
    }



}
