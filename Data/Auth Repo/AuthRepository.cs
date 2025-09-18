using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Auth_Repo
{
   public class AuthRepository:IAuthRepository
    {
        private readonly ProjectContext _context;

        public AuthRepository(ProjectContext context)
        {

            _context = context;
        }

        public async Task<Auth> LoginAsync(string usernameoremail, string password)
        {
            return await _context.Auths.FirstOrDefaultAsync(x => x.UserName == usernameoremail
                && x.Password == password);
        }

        public async Task<Auth> RegisterAsync(Auth auth)
        {
            await _context.Auths.AddAsync(auth);
            await _context.SaveChangesAsync();
            return auth;
        }

        public async Task<Auth> GetByEmailAsync(string email)
        {
            return await _context.Auths.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Auth> GetByUserNameAsync(string userName)
        {
            return await _context.Auths.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<Auth> GetByUsernameAsync(string username)
        {
           return await _context.Auths.FindAsync(username);
        }
    }
}
