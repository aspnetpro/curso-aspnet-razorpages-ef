using System.ComponentModel.DataAnnotations;

namespace AspNet.Blog.Web.Models.FormModel;

public record CommentFormModel
{
    public int PostId { get; set; }

    [Required]
    [StringLength(200)]
    public string? Author { get; set; }

    [Required]
    [StringLength(1000)]
    public string? Content { get; set; }
}
