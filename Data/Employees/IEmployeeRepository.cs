using Domain;

namespace Data.Employees;

public interface IEmployeeRepository
{
    Task<Employee> CreateEmployee(Employee employee);
    Task<Employee> GetById(int id);
    Task<List<Employee>> GetAllEmployee();
    Task DeleteEmployee(int id);
}
