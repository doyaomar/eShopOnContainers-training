namespace Catalog.API.Infrastructure.Services;

public class FileService : IFileService
{
    public string PathGetExtension(string path) => Path.GetExtension(path).ToLowerInvariant();
    public string PathCombine(string path1, string path2, string path3) => Path.Combine(path1, path2, path3);
    public FileStream FileCreate(string path) => File.Create(path);
    public bool FileExists(string path) => File.Exists(path);
    public Task<byte[]> FileReadAllBytesAsync(string path, CancellationToken cancellationToken = default)
    => File.ReadAllBytesAsync(path, cancellationToken);
}