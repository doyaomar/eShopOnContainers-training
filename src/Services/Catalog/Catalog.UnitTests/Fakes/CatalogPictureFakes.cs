namespace Catalog.UnitTests.Fakes;

internal static class CatalogPictureFakes
{
    internal static PictureFile GetPictureFileFake() => new() { Buffer = new byte[] { 0x00, 0x01, 0x02, 0x03 }, ContentType = "image/png" };

    internal static UploadPicture.Command GetUploadPictureCommandFake(Guid id)
    {
        var file = new Mock<IFormFile>();
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } });
        writer.Flush();
        stream.Position = 0;
        file.Setup(x => x.Length).Returns(100);
        file.Setup(x => x.FileName).Returns("path.png");
        file.Setup(x => x.ContentType).Returns("image/png");
        file.Setup(x => x.OpenReadStream()).Returns(stream);
        file.Setup(x => x.CopyToAsync(It.IsAny<Stream>(), CancellationToken.None)).Returns(Task.CompletedTask);

        return new UploadPicture.Command { Id = id, PictureFile = file.Object };
    }
}