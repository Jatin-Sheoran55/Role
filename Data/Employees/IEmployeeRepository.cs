using Domain;

namespace Data.Employees;

public interface IEmployeeRepository
{
    Task<Employee> CreateEmployee(Employee employee);
    Task<Employee> GetById(int id);
    Task<Employee> GetByEmail(string email);
    Task<List<Employee>> GetAllEmployee();
    Task DeleteEmployee(int id);

    Task<UserLogin?> LoginAsync(string userNameOrEmail, string password);
}
