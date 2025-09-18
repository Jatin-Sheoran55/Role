using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Roles.DTO;

namespace Application.Auth_Serv
{
    public interface IAuthApplication
    {
        Task<CreateAuthUpdateDto> RegisterAsync(AuthDto dto);
        Task<LoginResponseDto> LoginAsync(LoginDto dto);
    }
}
