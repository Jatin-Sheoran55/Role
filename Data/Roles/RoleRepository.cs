using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Roles;

public class RoleRepository : IRoleRepository

{
    private readonly ProjectContext _context;

    public RoleRepository(ProjectContext context)
    {
        _context = context;
    }


    public async Task<Role> CreateRole(Role role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task DeleteRole(int id)
    {
        var role = await _context.Roles.FindAsync(id);
        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Role>> GetAllRoles()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<Role> GetById(int id)
    {
        return await _context.Roles.FindAsync(id);
    }
}
