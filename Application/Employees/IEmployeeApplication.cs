using Application.Roles.DTO;
using AuthWebApp.Service.UserLogins.Dto;

namespace Application.Employees;

public interface IEmployeeApplication
{
    Task<EmployeeDto> CreateEmployee(CreateEmployeeDto employee);

    Task<EmployeeDto> GetById(int id);
    Task<List<EmployeeDto>> GetAllEmployees();
    Task<string> DeleteEmployee(int id);
  
    Task<LoginResponseDto> LoginAsync(LoginDto dto);
}
