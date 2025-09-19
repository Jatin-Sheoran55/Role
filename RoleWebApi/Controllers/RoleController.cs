using Application.Roles;
using Application.Roles.DTO;
using Microsoft.AspNetCore.Mvc;

namespace RoleWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleApplication _role;

    public RoleController(IRoleApplication role)
    {
        _role = role;
    }

    [HttpPost]
    public async Task<int> Post(CreateUpdateRoleDto input)
    {
        var data = await _role.CreateRole(input);
        return data.Id;
    }
    [HttpGet]
    public async Task<List<RoleDto>> GetAll()
    {
        return await _role.GetAllRoles();
    }
    [HttpGet("{id}")]
    public async Task<RoleDto> Get(int id)
    {
        return await _role.GetById(id);
    }
    [HttpPut("{id}")]
    public async Task Put(int id, CreateUpdateRoleDto input)
    {
        await _role.GetById(id);
    }
    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _role.DeleteRole(id);
    }
}