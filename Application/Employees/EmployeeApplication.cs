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
        IRoleRepository roleRepository)
    {
        _employeeRepository = employeeRepository;
        _roleRepository = roleRepository;
    }

    public async Task<EmployeeDto> CreateEmployee(CreateEmployeeDto input)
    {
        var checkEmployee= await _employeeRepository.GetByEmail(input.Email);

        if(checkEmployee != null)
        {
            throw new Exception("Email Id already Exists");       
        }

        var employee = new Employee();
        employee.Name = input.Name;
        employee.Email = input.Email;
        employee.IsEnabled = true;
        employee.PasswordHash = input.Password; 
        employee.CreatedDate = DateTime.Now;
        employee.RoleId = 2;

        var result = await _employeeRepository.CreateEmployee(employee);
        
        var employeeDto = new EmployeeDto();
        
        employeeDto.Id = result.Id;

        return employeeDto;
    }

    public async Task<string> DeleteEmployee(int id)
    {
        await _employeeRepository.DeleteEmployee(id);
        return "Deleted";
    }

    public async Task<List<EmployeeDto>> GetAllEmployees()
    {
        var data = await _employeeRepository.GetAllEmployee();
        var employees = data.Select(x => new EmployeeDto()
        {
            Id = x.Id,
            Name = x.Name,
            Email = x.Email,
            IsEnabled = x.IsEnabled,
            PasswordHash = x.PasswordHash,
            UpdatedDate = x.UpdatedDate,
            CreatedDate = x.CreatedDate,
        }).ToList();

        return employees;
    }

    public async Task<EmployeeDto> GetById(int id)
    {
        var result = await _employeeRepository.GetById(id);
        var data = new EmployeeDto()
        {
            Id = result.Id,
            Name = result.Name,
            Email = result.Email,
            IsEnabled = result.IsEnabled,
            PasswordHash = result.PasswordHash,
            UpdatedDate = result.UpdatedDate,
            CreatedDate = result.CreatedDate,

        };
        return data;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _employeeRepository.LoginAsync(dto.UserNameOrEmail, dto.Password);
       
        if (user == null)
            throw new Exception("Invalid username/email or password");

        var role = await _roleRepository.GetById(user.RoleId);

        return new LoginResponseDto
        {
            Id = user.Id,
            UserName = user.Name,
            Email = user.Email,
            Role = role.Name ?? "User"

        };
    }

}
