using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Employees;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ProjectContext _context;

    public EmployeeRepository(ProjectContext context)
    {
        _context = context;
    }

    public async Task<Employee> CreateEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee?> GetByEmail(string email)
    {

        return await _context.Employees
            .FirstOrDefaultAsync(x => x.Email == email);
    }


    public async Task<Employee?> LoginAsync(string email, string password)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(x => x.Email == email
        && x.PasswordHash == password);

    }
}

