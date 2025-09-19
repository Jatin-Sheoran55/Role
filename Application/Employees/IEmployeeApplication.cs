
using Application.Roles.DTO;
using AuthWebApp.Service.UserLogins.Dto;

public interface IEmployeeApplication
{
    Task<int> CreateEmployee(CreateEmployeeDto input);
    Task<LoginResponseDto> LoginAsync(LoginDto dto);

}
