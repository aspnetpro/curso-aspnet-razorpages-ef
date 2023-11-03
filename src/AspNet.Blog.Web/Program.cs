using AspNet.Blog.Web.Infrastructure.Data;
using AspNet.Blog.Web.Infrastructure.Storage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeAreaFolder("Admin", "/Manage");
    options.Conventions.AllowAnonymousToFolder("/Auth");
});

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

// Add compression response
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});
builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.SmallestSize;
});

// Add Azure Storage
builder.Services.AddOptions<AzureBlobStorageOptions>()
    .Bind(builder.Configuration.GetSection(AzureBlobStorageOptions.Name));
builder.Services.AddTransient<IStorage, AzureBlobStorageImpl>();

// Add Fluent Email
builder.Services
    .AddFluentEmail("fromemail@test.test")
    .AddRazorRenderer()
    .AddSmtpSender("localhost", 25);

// Add authentication
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie((opt) =>
    {
        opt.LoginPath = "/admin";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
