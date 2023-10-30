using AspNet.Blog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNet.Blog.Data;

public class BlogContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Category> Categories { get; set; }

    public BlogContext(DbContextOptions<BlogContext> dbContextOptions) 
        : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // registra todos os mapeamentos
        var assembly = typeof(BlogContext).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        base.OnModelCreating(modelBuilder);
    }
}
