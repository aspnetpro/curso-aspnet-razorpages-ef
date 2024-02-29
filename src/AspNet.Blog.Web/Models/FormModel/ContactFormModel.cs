using System.ComponentModel.DataAnnotations;

namespace AspNet.Blog.Web.Models.FormModel;

public record ContactFormModel
{
    [Required]
    [StringLength(200)]
    public string? Name { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string? Email { get; set; }

    [Required]
    [StringLength(1000)]
    public string? Message { get; set; }
}
