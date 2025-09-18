using Application.Roles.DTO;
using Data.Roles;
using Domain;

namespace Application.Roles
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<RoleDto> CreateRole(CreateUpdateRoleDto input)
        {
            var role = new Role();
            role.Name = input.Name;
            //role.Id = input.Id;
            role.Createddate = DateTime.Now;
            var result = await _roleRepository.CreateRole(role);

            var roleDto = new RoleDto();

            roleDto.Id = result.Id;

            return roleDto;
        }

        public async Task<string> DeleteRole(int id)
        {
            await _roleRepository.DeleteRole(id);
            return "Deleted";
        }

        public async Task<List<RoleDto>> GetAllRoles()
        {
           var data= await _roleRepository.GetAllRoles();
            var roles = data.Select(x => new RoleDto()
            {
                Id = x.Id,
                Name = x.Name,
                Createddate = x.Createddate,
                Updateddate = x.Updateddate,
            }).ToList();

            return roles;
        }

        public async Task<RoleDto> GetById(int id)
        {
              var result=  await _roleRepository.GetById(id);
            var data = new RoleDto()
            {
                Id = result.Id,
                Name = result.Name,
                Createddate =result.Createddate,
                Updateddate = result.Updateddate,
            };

            return data;
        }

       
    }
}
