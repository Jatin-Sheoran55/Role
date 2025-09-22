using Domain;

namespace Data.Employees;

public interface IEmployeeRepository
{

    Task<Employee> CreateEmployee(Employee employee);

    Task<Employee?> GetByEmail(string email);

    Task<Employee?> GetByIdAndPassword(int id, string password );

    Task UpdateEmployee(Employee input);

    Task<Employee?> LoginAsync(string email, string password);

    Task<string> ResetPasswordCode(string emailId, int UserId, string ipAddress);
}