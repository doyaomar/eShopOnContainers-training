namespace Catalog.API.Extensions;

internal static class FormFileExtensions
{
    internal static bool HasValidExtension(this IFormFile file) => file.FileName.HasValidExtension();

    internal static bool HasValidExtension(this string fileName)
    {
        string[] permittedExtensions = { ".png", ".gif", ".jpg", ".jpeg", ".bmp", ".tiff", ".wmf", ".jp2" };
        string? extension = Path.GetExtension(fileName)?.ToLowerInvariant();

        return !string.IsNullOrEmpty(extension) && permittedExtensions.Contains(extension);
    }

    internal static bool HasValidSizeLimit(this IFormFile file, long sizeLimit) => file.Length < sizeLimit;

    internal static bool HasValidSignature(this IFormFile file)
    {
        var fileSignature = new Dictionary<string, List<byte[]>>
        {
            { ".gif", new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } } },
            { ".png", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
            { ".bmp", new List<byte[]>{ new byte[] { 0x42, 0x4D} } },
            { ".wmf", new List<byte[]>{ new byte[] { 0xD7 ,0xCD ,0xC6 ,0x9A} } },
            { ".jp2", new List<byte[]> { new byte[] { 0x00, 0x00, 0x00, 0x0C, 0x6A, 0x50, 0x20, 0x20 } } },
            { ".jpeg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                }
            },
            { ".jpg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                }
            },
            { ".tiff", new List<byte[]>
                {
                    new byte[]{0x49, 0x20, 0x49 },
                    new byte[]{0x49 ,0x49 ,0x2A ,0x00 },
                    new byte[]{0x4D, 0x4D ,0x00 ,0x2A },
                    new byte[]{0x4D, 0x4D ,0x00 ,0x2A }
                }
            }
        };
        string ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        var signatures = fileSignature[ext];
        using var reader = new BinaryReader(file.OpenReadStream());
        byte[] headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

        return signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature));
    }
}
