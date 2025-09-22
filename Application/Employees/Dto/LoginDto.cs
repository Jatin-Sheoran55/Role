using System.ComponentModel.DataAnnotations;

namespace AuthWebApp.Service.UserLogins.Dto;

public class LoginDto
{
    [Required(ErrorMessage = "UserName or Email is required")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}