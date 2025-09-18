using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Roles.DTO;

namespace Application.Employee_Serv
{
    public interface IEmployeeApplication
    {
        Task<EmployeeDto> CreateEmployee(CreateUpdateEmployeeDto employee);

        Task<EmployeeDto> GetById(int id);
        Task<List<EmployeeDto>> GetAllEmployees();
        Task<string> DeleteEmployee(int id);
    }
}
