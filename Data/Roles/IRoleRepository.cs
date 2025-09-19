using Domain;

namespace Data.Roles;

public interface IRoleRepository
{
    Task<Role> CreateRole(Role role);
    Task<Role> GetById(int id);
    Task<List<Role>> GetAllRoles();
    Task DeleteRole(int id);
}
