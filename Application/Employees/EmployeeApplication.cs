using Application.Employees.Dto;
using Application.Roles.DTO;
using AuthWebApp.Service.UserLogins.Dto;
using Data.Employees;
using Data.Roles;
using Domain;
using YourAppNamespace.Models;

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

        var response = await _employeeRepository.CreateEmployee(employee);

        return response.Id;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _employeeRepository.LoginAsync(dto.Email, dto.Password);

        if (user == null)
        {
            throw new Exception("Invalid username/email or password");
        }

        var role = await _roleRepository.GetById(user.RoleId);

        return new LoginResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = role.Name

        };
    }

    public async Task<bool> ChangePasswordAsync(int id, ChangePasswordDto dto)
    {
        var employee = await _employeeRepository.GetByIdAndPassword(id, dto.OldPassword);
         

        if (employee == null)
        {
            throw new Exception("Employee Not Found !");
        }



        employee.PasswordHash = dto.NewPassword;
             

        await _employeeRepository.UpdateEmployee(employee);

        return true;
       
    }

    public async Task<string> ForgetPasswordAsync(string emailId, string ipAddress)
    {
        var checkEmployee = await _employeeRepository.GetByEmail(emailId);

        if (checkEmployee == null)
        {
            throw new Exception("Email Id not Exists");
        }

        var code = await _employeeRepository.ResetPasswordCode(emailId, checkEmployee.Id, ipAddress);
            
        return code;

    }

     public async Task  ResetPassword(ResetPasswordDto input)
    {
        var result =  await _employeeRepository.ValidateResetPasswordCode(input.Code);

        if (result == null)
        {
            throw new Exception(" code is not valid");
        }

        var employee =  await _employeeRepository.GetById(result.UserId);

        if(employee == null) 
            {
            throw new Exception("Employee not found");
            }

           employee.PasswordHash = input.Password;

        await _employeeRepository.UpdateEmployee(employee);


         result.Status = ResetPasswordStatus.Used;

        await _employeeRepository.UpdateResetPasswordCode(result);

    }
}














