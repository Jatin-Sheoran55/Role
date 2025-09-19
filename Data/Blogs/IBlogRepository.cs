using Domain;

namespace Data.Blogs;

public interface IBlogRepository
{
    Task<Blog> CreateBlog(Blog blog);
    Task<Blog> GetById(int id);
    Task<List<Blog>> GetAllBlog();
    Task DeleteBlog(int id);
}
