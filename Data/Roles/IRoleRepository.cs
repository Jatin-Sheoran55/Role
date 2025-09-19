using Domain;

namespace Data.Roles;

public interface IRoleRepository
{ 
    Task<Role> GetById(int id); 
}
