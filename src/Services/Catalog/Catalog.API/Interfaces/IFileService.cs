namespace Catalog.API.Interfaces;

public interface IFileService
{
    FileStream FileCreate(string path);
    bool FileExists(string path);
    Task<byte[]> FileReadAllBytesAsync(string path, CancellationToken cancellationToken = default);
    string PathCombine(string path1, string path2, string path3);
    string PathGetExtension(string path);
}