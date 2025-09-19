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

    public async Task UpdateEmployee(Employee input)
    {
        _context.Employees.Update(input);
        await _context.SaveChangesAsync();
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

    public async Task<Employee?>  GetByIdAndPassword(int id, string password)
    {
        return await _context.Employees
         .FirstOrDefaultAsync(x => x.Id == id && x.PasswordHash == password);

    }


}

