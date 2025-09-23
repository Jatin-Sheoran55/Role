using Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using YourAppNamespace.Models;

namespace Data.Employees;

public interface IEmployeeRepository
{

    Task<Employee> CreateEmployee(Employee employee);

    Task<Employee?> GetByEmail(string email);
    Task<Employee?> GetById(int id);

    Task<Employee?> GetByIdAndPassword(int id, string password );

    Task UpdateEmployee(Employee input);

    Task<Employee?> LoginAsync(string email, string password);

    
    
    Task<string> ResetPasswordCode(string emailId, int UserId, string ipAddress);

    Task<ResetPasswordCode?> ValidateResetPasswordCode(string code);

    Task UpdateResetPasswordCode(ResetPasswordCode input);

}



