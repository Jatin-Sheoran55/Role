using Domain;
using Microsoft.EntityFrameworkCore;
using YourAppNamespace.Models;

namespace Data;

public class ProjectContext : DbContext
{

    public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
    {

    }

    public DbSet<Role> Roles { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Blog> Blogs { get; set; }

    public DbSet<ResetPasswordCode> ResetPasswordCodes { get; set; }



}
