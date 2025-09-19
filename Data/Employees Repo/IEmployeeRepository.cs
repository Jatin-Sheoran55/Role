using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Data.Employees_Repo
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateEmployee(Employee employee);
        Task<Employee> GetById(int id);
        Task<List<Employee>> GetAllEmployee();
        Task DeleteEmployee(int  id);
        Task<Employee> GetByEmail(string email);
        Task LoginAsync(string userName, string password);
    }
}
