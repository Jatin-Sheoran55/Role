using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Data.Roles
{
   public interface IRoleRepository
    {
        Task<Role> CreateRole(Role role);
        Task<Role> GetById(int id);
        Task<List<Role>> GetAllRoles();
        Task DeleteRole(int id);
    }
}
