namespace AspNet.Blog.Web.Models.FormModel;

public record ContactFormModel
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Message { get; set; }
}
