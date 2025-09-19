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

    public async Task DeleteEmployee(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        _context.Employees.Remove(employee);
    }

    public async Task<List<Employee>> GetAllEmployee()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee> GetById(int id)
    {
        return await _context.Employees.FindAsync(id);
    }
}
