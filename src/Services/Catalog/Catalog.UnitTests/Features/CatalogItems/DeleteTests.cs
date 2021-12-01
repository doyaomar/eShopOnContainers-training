namespace Catalog.UnitTests.Features.CatalogItems;

public class DeleteTests
{
    readonly Mock<ICatalogDbContext> _dbStub;
    readonly Delete.Handler _handler;

    public DeleteTests()
    {
        _dbStub = new();
        _handler = new(_dbStub.Object);
    }

    public async Task Handle_WhenRequestIsValidAndProductExists_ThenReturnsTrue()
    {
        var validProductIdMock = Guid.NewGuid();
        Delete.Command validRequestStub = new(validProductIdMock);
        var validCancellationTokenStub = CancellationToken.None;
        var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
        _dbStub.Setup(db => db.CatalogItems.FindOneAndDeleteAsync(It.IsAny<Expression<Func<CatalogItem, bool>>>(),
                                                                  null,
                                                                  validCancellationTokenStub)).ReturnsAsync(catalogItemStub);

        var actual = await _handler.Handle(validRequestStub, validCancellationTokenStub);

        actual.Should().BeTrue();
    }

    public async Task Handle_WhenRequestIsValidAndProductDoesntExist_ThenReturnsFalse()
    {
        var validProductIdMock = Guid.NewGuid();
        Delete.Command validRequestStub = new(validProductIdMock);
        var validCancellationTokenStub = CancellationToken.None;
        CatalogItem catalogItemStub = null!;
        _dbStub.Setup(db => db.CatalogItems.FindOneAndDeleteAsync(It.IsAny<Expression<Func<CatalogItem, bool>>>(),
                                                                  null,
                                                                  validCancellationTokenStub)).ReturnsAsync(catalogItemStub);

        var actual = await _handler.Handle(validRequestStub, validCancellationTokenStub);

        actual.Should().BeFalse();
    }

    [Fact]
    public void Handle_WhenRequestIsNull_ThenThrowsException()
    {
        Delete.Command invalidRequestStub = null!;
        var validCancellationTokenStub = CancellationToken.None;

        Func<Task> actual = async () => await _handler.Handle(invalidRequestStub, validCancellationTokenStub);

        actual.Should().ThrowAsync<ArgumentNullException>();
    }
}