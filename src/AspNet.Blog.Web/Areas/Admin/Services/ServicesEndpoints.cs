using AspNet.Blog.Web.Common;
using AspNet.Blog.Web.Infrastructure.Data;
using AspNet.Blog.Web.Infrastructure.Storage;
using Microsoft.AspNetCore.Mvc;

namespace AspNet.Blog.Web.Areas.Admin.Services;

public static class ServicesEndpoints
{
    public static void MapAdminServiceEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/admin/svc/categories/autocomplete", GetCategoriesAutocomplete);
        app.MapPost("/admin/svc/upload", Upload)
            .DisableAntiforgery();
    }

    private static async Task<IResult> GetCategoriesAutocomplete(
        BlogContext blogContext,
        [FromQuery] string term)
    {
        string slug = term.ToSlug();

        var model = blogContext.Categories
            .Where(x => x.Permalink.StartsWith(slug))
            .Select(x => x.Name)
            .ToList();

        return Results.Ok(model);
    }

    private static async Task<IResult> Upload(IFormFile file,
        IStorage storage)
    {
        string fileName = String.Concat(Path.GetRandomFileName().ToSlug(), Path.GetExtension(file.FileName));
        string fileLink = await storage.UploadAsync(file.OpenReadStream(), fileName);

        var model = new
        {
            filelink = fileLink,
            title = fileName
        };

        return Results.Ok(model);
    }
}
