using Application.Employee_Serv;
using Application.Roles.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RoleWebApi.Controllers
{
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
