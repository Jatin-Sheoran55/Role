using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Roles.DTO
{
    public class CreateAuthUpdateDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get;  set; }
        public string Token { get; set; }
        public int Id { get; set; }
    }
}
