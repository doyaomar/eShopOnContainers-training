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

    [Fact]
    public async Task Handle_WhenRequestIsValidAndProductExists_ThenReturnsTrue()
    {
        var validProductIdMock = Guid.NewGuid();
        Delete.Command validRequestStub = new(validProductIdMock);
        var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
        _dbStub.Setup(db => db.FindOneAndDeleteAsync(validProductIdMock, CancellationToken.None))
            .ReturnsAsync(catalogItemStub);

        var actual = await _handler.Handle(validRequestStub, CancellationToken.None);

        actual.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_WhenRequestIsValidAndProductDoesntExist_ThenReturnsFalse()
    {
        var validProductIdMock = Guid.NewGuid();
        Delete.Command validRequestStub = new(validProductIdMock);
        CatalogItem catalogItemStub = null!;
        _dbStub.Setup(db => db.FindOneAndDeleteAsync(validProductIdMock, CancellationToken.None))
            .ReturnsAsync(catalogItemStub);

        var actual = await _handler.Handle(validRequestStub, CancellationToken.None);

        actual.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_WhenRequestIsNull_ThenThrowsException()
    {
        Delete.Command invalidRequestStub = null!;

        Func<Task> actual = async () => await _handler.Handle(invalidRequestStub, CancellationToken.None);

        await actual.Should().ThrowAsync<ArgumentNullException>();
    }
}