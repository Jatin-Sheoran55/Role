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
     
    public async Task<Role> GetById(int id)
    {
        return await _context.Roles.FindAsync(id);
    }
}
