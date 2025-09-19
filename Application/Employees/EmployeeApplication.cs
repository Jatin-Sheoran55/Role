using Application.Roles.DTO;
using AuthWebApp.Service.UserLogins.Dto;
using Data.Employees;
using Data.Roles;
using Domain;

namespace Application.Employees;

public class EmployeeApplication : IEmployeeApplication
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IRoleRepository _roleRepository;
     
    public EmployeeApplication(IEmployeeRepository employeeRepository,
        IRoleRepository roleRepository
         )
    {
        _employeeRepository = employeeRepository;
        _roleRepository = roleRepository;


    }

    public async Task<int> CreateEmployee(CreateEmployeeDto input)
    {
        var checkEmployee = await _employeeRepository.GetByEmail(input.Email);

        if (checkEmployee != null)
        {
            throw new Exception("Email Id already Exists");
        }

        var employee = new Employee();
        employee.Name = input.Name;
        employee.Email = input.Email;
        employee.PasswordHash = input.Password;

        employee.CreatedDate = DateTime.Now;
        employee.RoleId = 2;
        employee.IsEnabled = true;

        var response = await _employeeRepository.CreateEmployee(employee);

        return response.Id;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _employeeRepository.LoginAsync(dto.Email, dto.Password);

        if (user == null)
            throw new Exception("Invalid username/email or password");

        var role = await _roleRepository.GetById(user.RoleId);

        return new LoginResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = role.Name

        };
    }

}














