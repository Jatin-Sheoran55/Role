using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Blog_Repo
{
    public class BlogRepository:IBlogRepository
    {
        private readonly ProjectContext _context;

        public BlogRepository(ProjectContext context)
        {
            _context = context;
        }

        public async Task<Blog> CreateBlog(Blog blog)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return blog;
        }

        public async Task DeleteBlog(int id)
        {

            var blog = await _context.Blogs.FindAsync(id);
            _context.Blogs.Remove(blog);
        }

        public async Task<List<Blog>> GetAllBlog()
        {
            return await _context.Blogs.ToListAsync();
        }

        public async Task<Blog> GetById(int id)
        {
            return await _context.Blogs.FindAsync();
        }
    }
}
