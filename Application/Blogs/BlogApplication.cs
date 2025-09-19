using Application.Roles.DTO;
using Data.Blogs;
using Domain;

namespace Application.Blogs;

public class BlogApplication : IBlogApplication
{
    private readonly IBlogRepository _blogRepository;

    public BlogApplication(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    public async Task<BlogDto> CreateBlog(CreateUpdateBlogDto input)
    {
        var blog = new Blog();
        blog.Title = input.Title;
        blog.Description = input.Description;
        blog.UpdatedDate = DateTime.Now;
        blog.CreatedDate = DateTime.Now;
        blog.EmployeeId = input.EmployeeId;
        var result = await _blogRepository.CreateBlog(blog);
        var blogDto = new BlogDto();
        blogDto.Id = result.Id;

        return blogDto;
    }

    public async Task<string> DeleteBlog(int id)
    {
        await _blogRepository.DeleteBlog(id);
        return "Deleted";
    }

    public async Task<List<BlogDto>> GetAllBlogs()
    {
        var data = await _blogRepository.GetAllBlog();
        var blogs = data.Select(x => new BlogDto()
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            UpdatedDate = x.UpdatedDate,
            CreatedDate = x.CreatedDate,
        }).ToList();

        return blogs;
    }

    public async Task<BlogDto> GetById(int id)
    {
        var result = await _blogRepository.GetById(id);
        var data = new BlogDto()
        {
            Id = result.Id,
            Title = result.Title,
            Description = result.Description,
            UpdatedDate = result.UpdatedDate,
            CreatedDate = result.CreatedDate,

        };
        return data;
    }
}
