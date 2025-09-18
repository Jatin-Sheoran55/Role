using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Data.Blog_Repo
{
   public interface IBlogRepository
    {
        Task<Blog> CreateBlog(Blog blog);
        Task<Blog> GetById(int id);
        Task<List<Blog>> GetAllBlog ();
        Task DeleteBlog(int id);
    }
}
