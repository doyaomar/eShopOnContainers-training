namespace Catalog.UnitTests.Features.CatalogItems;

public class UpdateTests
{
    private readonly Mock<ICatalogDbContext> _dbStub;
    private readonly Update.Handler _handler;
    private readonly Mock<IMapper> _mapperStub;
    private readonly Mock<IFileService> _fileServiceStub;
    private readonly IValidator<Update.Command> _validator;

    public UpdateTests()
    {
        _mapperStub = new();
        _dbStub = new();
        _fileServiceStub = new();
        _validator = new UpdateValidator();
        _handler = new(_dbStub.Object, _mapperStub.Object, _fileServiceStub.Object, _validator);
    }

    [Fact]
    public async Task Handle_WhenCommandIsValidAndProductExists_ThenReturnsTrue()
    {
        Update.Command validCommandStub = CatalogItemFakes.GetUpdateCommandFake(Guid.NewGuid());
        var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
        _mapperStub.Setup(mapper => mapper.Map<CatalogItem>(validCommandStub)).Returns(catalogItemStub);
        _fileServiceStub.Setup(svc => svc.PathGetExtension(It.IsAny<string>())).Returns(".png");
        _dbStub.Setup(db => db.FindOneAndReplaceAsync(catalogItemStub, CancellationToken.None)).ReturnsAsync(catalogItemStub);

        var actual = await _handler.Handle(validCommandStub, CancellationToken.None);

        actual.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_WhenCommandIsValidAndProductDoesntExist_ThenReturnsFalse()
    {
        Update.Command validCommandStub = CatalogItemFakes.GetUpdateCommandFake(Guid.NewGuid());
        var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
        _mapperStub.Setup(mapper => mapper.Map<CatalogItem>(validCommandStub)).Returns(catalogItemStub);
        _fileServiceStub.Setup(svc => svc.PathGetExtension(It.IsAny<string>())).Returns(".png");
        _dbStub.Setup(db => db.FindOneAndReplaceAsync(catalogItemStub, CancellationToken.None)).ReturnsAsync((CatalogItem)null!);

        var actual = await _handler.Handle(validCommandStub, CancellationToken.None);

        actual.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_WhenCommandIsNull_ThenThrowsValidationException()
    {
        Update.Command invalidCommandStub = null!;

        Func<Task> actual = async () => await _handler.Handle(invalidCommandStub, CancellationToken.None);

        await actual.Should().ThrowAsync<ArgumentNullException>();
    }
}