using AspNet.Blog.Web.Areas.Admin.Services;
using AspNet.Blog.Web.Infrastructure.Data;
using AspNet.Blog.Web.Infrastructure.Storage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add entity framework
builder.Services.AddDbContext<BlogContext>((optionsAction) =>
{
    var connString = builder.Configuration.GetConnectionString("Database");
    optionsAction.UseSqlServer(connString, (sqlServerOptionsAction) =>
    {
        sqlServerOptionsAction.EnableRetryOnFailure(maxRetryCount: 3);
    });
});

// Add cache
builder.Services.AddMemoryCache();

// Add response caching
//builder.Services.AddResponseCaching();

// Add compression response
//builder.Services.AddResponseCompression(options =>
//{
//    options.EnableForHttps = true;
//    options.Providers.Add<BrotliCompressionProvider>();
//    options.Providers.Add<GzipCompressionProvider>();
//});
//builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
//{
//    options.Level = CompressionLevel.Fastest;
//});
//builder.Services.Configure<GzipCompressionProviderOptions>(options =>
//{
//    options.Level = CompressionLevel.SmallestSize;
//});

// Add Azure Storage
builder.Services.AddOptions<AzureBlobStorageOptions>()
    .Bind(builder.Configuration.GetSection(AzureBlobStorageOptions.Name));
builder.Services.AddTransient<IStorage, AzureBlobStorageImpl>();

// Add Fluent Email
builder.Services
    .AddFluentEmail("sender@mbanagouro.com.br", "ASP.NET PRO BLOG")
    .AddRazorRenderer()
    .AddSmtpSender(host: "smtp.mailgun.org",
        port: 587,
        username: "postmaster@mbanagouro.com.br",
        password: "85c17b6273ed8ac3a5b9c3096f47f366-b02bcf9f-955f019d");

// Add authentication
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie((opt) =>
    {
        opt.LoginPath = "/admin";
    });

builder.Services.AddOutputCache(options => {
    options.AddPolicy("default", b => b.Expire(TimeSpan.FromHours(1)));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseResponseCompression();
//app.UseResponseCaching();
app.UseHttpsRedirection();

var cacheMaxAgeOneWeek = (60 * 60 * 24 * 7).ToString();
app.UseStaticFiles(new StaticFileOptions
{
    HttpsCompression = HttpsCompressionMode.Compress,
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append(
             "Cache-Control", $"public, max-age={cacheMaxAgeOneWeek}");
    }
});

app.UseRouting();
app.UseOutputCache();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapAdminServiceEndpoints();

app.Run();
