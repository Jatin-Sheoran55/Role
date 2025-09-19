using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Roles.DTO;
using Data.Employees_Repo;
using Domain;

namespace Application.Employee_Serv
{
  public class EmployeeApplication:IEmployeeApplication
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeApplication(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto> CreateEmployee(CreateUpdateEmployeeDto input)
        {
            var email = await _employeeRepository.GetByEmail(input.Email);
                if (email == null)
            {
                throw new Exception("Email id is All Ready Exixted");
            }
               

            
          

            var employee = new Employee();
            employee.Name = input.Name;
            employee.Email = input.Email;
            employee.IsEnabled = input.IsEnabled;
            employee.PasswordHash = input.PasswordHash;
          
            employee.CreatedDate = DateTime.Now;
            employee.RoleId = 3;
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
            var employees = data.Select(x => new EmployeeDto ()
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

        public async Task<EmployeeDto> GetByIEmail(string email)
        {
            var result = await _employeeRepository.GetByEmail(email);
            var data = new EmployeeDto()
            {
                Id = result.Id,
                Email = result.Email,
            };
            return data;
        }

        public async Task<LoginDto> LoginAsync(LoginDto dto)
        {
            var user = await _employeeRepository.LoginAsync(dto.UserName, dto.Password);
            if (user == null)
            {
                user = new LoginResponseDto
                {
                    UserName = user.UserName,
                    Password = user.Password,
                };
            }
        }

        public Task<EmployeeDto> RegisterAsync(CreateUpdateEmployeeDto dto)
        {
            throw new NotImplementedException();
        }

        Task<LoginResponseDto> IEmployeeApplication.LoginAsync(LoginDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
