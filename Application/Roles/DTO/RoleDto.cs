using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Roles.DTO
{
   public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Createddate { get; set; }
        public DateTime Updateddate { get; set; }
    }
}
