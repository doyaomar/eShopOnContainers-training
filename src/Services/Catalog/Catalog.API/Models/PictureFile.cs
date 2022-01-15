namespace Catalog.API.Models;

public class PictureFile
{
    public byte[] Buffer { get; set; } = default!;

    public string ContentType { get; set; } = default!;
}