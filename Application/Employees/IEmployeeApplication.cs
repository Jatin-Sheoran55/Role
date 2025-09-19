using Application.Roles.DTO;

namespace Application.Employees;

public interface IEmployeeApplication
{
    Task<EmployeeDto> CreateEmployee(CreateUpdateEmployeeDto employee);

    Task<EmployeeDto> GetById(int id);
    Task<List<EmployeeDto>> GetAllEmployees();
    Task<string> DeleteEmployee(int id);
}
