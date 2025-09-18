using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Roles.DTO
{
   public class CreateUpdateBlogDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int EmployeeId { get; set; }
    }
}
