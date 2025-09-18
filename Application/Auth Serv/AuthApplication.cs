using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Roles.DTO;
using Data.Auth_Repo;
using Domain;

namespace Application.Auth_Serv
{
   public class AuthApplication : IAuthApplication
    {
        private readonly IAuthRepository _authRepository;

        public AuthApplication(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
            var auth = await _authRepository.LoginAsync(dto.UserName, dto.Password);
            if (auth == null)
                throw new Exception("Invalid UserName pr Password");

            return new LoginResponseDto
            {
                UserName = auth.UserName,
                Password = auth.Password,
                Email = auth.Email,
            };
        }

        public async Task<CreateAuthUpdateDto> RegisterAsync(AuthDto dto)
        {
            var existingAuth= await _authRepository.GetByEmailAsync(dto.Email);
            if (existingAuth != null)
                throw new Exception("Email All Ready Registered");

            var existingUserName = await _authRepository.GetByUsernameAsync(dto.UserName);
            if (existingUserName != null)
                throw new Exception("UserName Allready Taken");

            var auth = new Auth
            {
                UserName = dto.UserName,

                Password = dto.Password,
                Email = dto.Email,
                CreateDate = DateTime.Now,

            };

            var CreatedAuth = await _authRepository.RegisterAsync(auth);

            return new CreateAuthUpdateDto
            {
                UserName = CreatedAuth.UserName,
                //Email = CreatedAuth.Email,
                Password = CreatedAuth.Password,
                CreateDate = CreatedAuth.CreateDate,
            };
        }
    }
}
