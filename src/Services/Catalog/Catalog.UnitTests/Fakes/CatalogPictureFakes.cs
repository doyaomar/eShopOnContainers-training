namespace Catalog.UnitTests.Fakes;

internal static class CatalogPictureFakes
{
    internal static PictureFile GetPictureFileFake()
    => new PictureFile { Buffer = new byte[] { 0x00, 0x01, 0x02, 0x03 }, ContentType = "image/png" };

    internal static UploadPicture.Command GetUploadPictureCommandFake(Guid id)
    {
        var file = new Mock<IFormFile>();
        file.Setup(x => x.CopyToAsync(It.IsAny<Stream>(), CancellationToken.None)).Returns(Task.CompletedTask);

        return new UploadPicture.Command { Id = id, PictureFile = file.Object };
    }
}