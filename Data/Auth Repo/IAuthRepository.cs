using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Data.Auth_Repo
{
    public interface IAuthRepository
    {
        Task<Auth> RegisterAsync(Auth auth);
        Task<Auth> LoginAsync(string usernameorEmail, string password);
        Task<Auth> GetByEmailAsync(string email);
        Task<Auth> GetByUsernameAsync(string username);
    }
}
