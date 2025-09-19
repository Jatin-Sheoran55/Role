namespace Application.Roles.DTO;

public class CreateUpdateEmployeeDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string IsEnabled { get; set; }
    //public int RoleId { get; set; }
}
