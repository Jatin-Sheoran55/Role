using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Roles.DTO;

namespace Application.Blog_Serv
{
    public interface IBlogApplication
    {
        Task<BlogDto> CreateBlog(CreateUpdateBlogDto blog);

        Task<BlogDto> GetById(int id);
        Task<List<BlogDto>> GetAllBlogs();
        Task<string> DeleteBlog(int id);
    }
}
