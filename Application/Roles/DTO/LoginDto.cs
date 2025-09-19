using System.ComponentModel.DataAnnotations;

namespace Application.Roles.DTO;

public class LoginDto
{
    [Required(ErrorMessage = "UserName  is required")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
