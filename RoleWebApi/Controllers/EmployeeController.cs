using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Employee_Serv;
using Application.Roles.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace RoleWebApi.Controllers
{
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

        [HttpPost("Registered")]
        public async Task<IActionResult> Register(CreateUpdateEmployeeDto dto)
        {
            try
            {
                var Response = await _employee.RegisterAsync(dto);
                return Ok(new { message = "User Registered Successfully", data = Response });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Employee(LoginDto dto)
        {
            try
            {
                var response = await _employee.LoginAsync(dto);

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

        

        [HttpPost]
        public async Task<int> Post(CreateUpdateEmployeeDto input)
        {
            var data = await _employee.CreateEmployee(input);
            return data.Id;
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
        public async Task Put(int id,CreateUpdateEmployeeDto input)
        {
            await _employee.GetById(id);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _employee.DeleteEmployee(id);
        }


        
    }
}
