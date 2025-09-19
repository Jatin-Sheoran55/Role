using Application.Roles.DTO;

namespace Application.Roles;

public interface IRoleApplication
{
    Task<RoleDto> CreateRole(CreateUpdateRoleDto role);

    Task<RoleDto> GetById(int id);
    Task<List<RoleDto>> GetAllRoles();
    Task<string> DeleteRole(int id);
}
