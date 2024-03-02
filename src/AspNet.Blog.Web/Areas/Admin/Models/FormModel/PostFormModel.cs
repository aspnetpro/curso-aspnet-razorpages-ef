using AspNet.Blog.Web.Common;
using System.ComponentModel.DataAnnotations;

namespace AspNet.Blog.Web.Areas.Admin.Models.FormModel;

public record PostFormModel
{
    public int? PostId { get; set; }
    
    public string Permalink => Title.ToSlug();

    [Required]
    [StringLength(70)]
    public string Title { get; set; }

    [Required]
    [StringLength(70)]
    public string Category { get; set; }

    [Required]
    [StringLength(500)]
    public string Summary { get; set; }

    [Required]
    [StringLength(int.MaxValue)]
    public string Content { get; set; }

    public string Tags { get; set; }
}
