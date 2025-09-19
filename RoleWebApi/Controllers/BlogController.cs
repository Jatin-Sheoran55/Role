using Application.Blogs;
using Application.Roles.DTO;
using Microsoft.AspNetCore.Mvc;

namespace RoleWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly IBlogApplication _blog;

    public BlogController(IBlogApplication blog)
    {
        _blog = blog;
    }

    [HttpPost]
    public async Task<int> Post(CreateUpdateBlogDto input)
    {
        var data = await _blog.CreateBlog(input);
        return data.Id;
    }
    [HttpGet]
    public async Task<List<BlogDto>> GetAll()
    {
        return await _blog.GetAllBlogs();
    }
    [HttpGet("{id}")]

    public async Task<BlogDto> Get(int id)
    {
        return await _blog.GetById(id);
    }

    [HttpPut("{id}")]
    public async Task Put(int id, CreateUpdateBlogDto input)
    {
        await _blog.GetById(id);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _blog.DeleteBlog(id);
    }



}
