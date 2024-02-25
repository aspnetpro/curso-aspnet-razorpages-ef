using AspNet.Blog.Web.Common;
using AspNet.Blog.Web.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace AspNet.Blog.Web.Areas.Admin.Services;

public static class ServicesEndpoints
{
    public static void MapAdminServiceEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/admin/svc/categorias/autocomplete", GetCategoriesAutocomplete);
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
}
