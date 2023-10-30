namespace AspNet.Blog.Web.Infrastructure.Storage;

public class AzureBlobStorageOptions
{
    public const string Name = "AzureBlobStorage";

    public string ConnectionString { get; set; }
    public string BlobName { get; set; }
    public string BaseUrl { get; set; }
}
