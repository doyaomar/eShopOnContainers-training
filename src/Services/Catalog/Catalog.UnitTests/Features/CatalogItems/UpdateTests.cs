namespace Catalog.UnitTests.Features.CatalogItems;

public class UpdateTests
{
    readonly Mock<ICatalogDbContext> _dbStub;
    readonly Update.Handler _handler;
    readonly Mock<IMapper> _mapperStub;

    public UpdateTests()
    {
        _mapperStub = new();
        _dbStub = new();
        _handler = new(_dbStub.Object, _mapperStub.Object);
    }

    public async Task Handle_WhenRequestIsValidAndProductExists_ThenReturnsTrue()
    {
        Update.Command validRequestStub = new();
        var validCancellationTokenStub = CancellationToken.None;
        var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
        _mapperStub.Setup(mapper => mapper.Map<CatalogItem>(validRequestStub)).Returns(catalogItemStub);
        _dbStub.Setup(db => db.CatalogItems.FindOneAndReplaceAsync(It.IsAny<Expression<Func<CatalogItem, bool>>>(),
                                                                  catalogItemStub,
                                                                  null,
                                                                  validCancellationTokenStub)).ReturnsAsync(catalogItemStub);

        var actual = await _handler.Handle(validRequestStub, validCancellationTokenStub);

        actual.Should().BeTrue();
    }

    public async Task Handle_WhenRequestIsValidAndProductDoesntExist_ThenReturnsFalse()
    {
        Update.Command validRequestStub = new();
        var validCancellationTokenStub = CancellationToken.None;
        var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
        _mapperStub.Setup(mapper => mapper.Map<CatalogItem>(validRequestStub)).Returns(catalogItemStub);
        _dbStub.Setup(db => db.CatalogItems.FindOneAndReplaceAsync(It.IsAny<Expression<Func<CatalogItem, bool>>>(),
                                                                  catalogItemStub,
                                                                  null,
                                                                  validCancellationTokenStub)).ReturnsAsync((CatalogItem)null!);

        var actual = await _handler.Handle(validRequestStub, validCancellationTokenStub);

        actual.Should().BeFalse();
    }

    [Fact]
    public void Handle_WhenRequestIsNull_ThenThrowsException()
    {
        Update.Command invalidRequestStub = null!;
        var validCancellationTokenStub = CancellationToken.None;

        Func<Task> actual = async () => await _handler.Handle(invalidRequestStub, validCancellationTokenStub);

        actual.Should().ThrowAsync<ArgumentNullException>();
    }
}