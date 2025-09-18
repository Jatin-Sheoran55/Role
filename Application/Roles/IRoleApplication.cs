using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Roles.DTO;
using Domain;

namespace Application.Roles
{
  public interface IRoleApplication
    {
        Task<RoleDto> CreateRole(CreateUpdateRoleDto role);
       
        Task<RoleDto> GetById(int id);
        Task<List<RoleDto>> GetAllRoles();
        Task<string> DeleteRole(int id);
    }
}
