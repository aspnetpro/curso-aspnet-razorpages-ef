using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;

namespace AspNet.Blog.Web.Infrastructure.Storage;

public class AzureBlobStorageImpl : IStorage
{
    private readonly BlobContainerClient blobContainerClient;
    private readonly IOptions<AzureBlobStorageOptions> azureBlobStorageOptions;

    public AzureBlobStorageImpl(IOptions<AzureBlobStorageOptions> azureBlobStorageOptions)
    {
        this.azureBlobStorageOptions = azureBlobStorageOptions;
        blobContainerClient = new BlobContainerClient(azureBlobStorageOptions.Value.ConnectionString, azureBlobStorageOptions.Value.BlobName);
    }

    public async Task<string> UploadAsync(Stream stream, string fileName, CancellationToken cancellationToken = default)
    {
        await blobContainerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
        var result = await blobContainerClient.UploadBlobAsync(fileName, stream, cancellationToken);
        return $"{azureBlobStorageOptions.Value.BaseUrl}/{azureBlobStorageOptions.Value.BlobName}/{fileName}";
    }
}
