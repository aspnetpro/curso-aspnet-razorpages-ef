using AspNet.Blog.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNet.Blog.Web.Infrastructure.Data.Mapping;

public class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Permalink)
            .HasMaxLength(70)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(70)
            .IsRequired();

        builder.HasMany(x => x.Posts)
            .WithOne(x => x.Category);
    }
}
