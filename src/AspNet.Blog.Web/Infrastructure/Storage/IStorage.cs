namespace AspNet.Blog.Web.Infrastructure.Storage;

public interface IStorage
{
    Task<string> UploadAsync(Stream stream, string fileName, CancellationToken cancellationToken = default);
}
