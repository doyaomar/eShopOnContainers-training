namespace Catalog.UnitTests.Features.CatalogItems;

public class DeleteTests
{
    private readonly Mock<ICatalogDbContext> _dbStub;
    private readonly Delete.Handler _handler;
    private readonly IValidator<Delete.Command> _validator;

    public DeleteTests()
    {
        _dbStub = new();
        _validator = new DeleteValidator();
        _handler = new(_dbStub.Object, _validator);
    }

    [Fact]
    public async Task Handle_WhenCommandIsValidAndProductExists_ThenReturnsTrue()
    {
        var validProductIdMock = Guid.NewGuid();
        Delete.Command validCommandStub = new(validProductIdMock);
        var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
        _dbStub.Setup(db => db.FindOneAndDeleteAsync(validProductIdMock, CancellationToken.None))
            .ReturnsAsync(catalogItemStub);

        var actual = await _handler.Handle(validCommandStub, CancellationToken.None);

        actual.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_WhenCommandIsValidAndProductDoesntExist_ThenReturnsFalse()
    {
        var validProductIdMock = Guid.NewGuid();
        Delete.Command validCommandStub = new(validProductIdMock);
        CatalogItem catalogItemStub = null!;
        _dbStub.Setup(db => db.FindOneAndDeleteAsync(validProductIdMock, CancellationToken.None))
            .ReturnsAsync(catalogItemStub);

        var actual = await _handler.Handle(validCommandStub, CancellationToken.None);

        actual.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_WhenCommandIsNull_ThenThrowsValidationException()
    {
        Delete.Command invalidCommandStub = null!;

        Func<Task> actual = async () => await _handler.Handle(invalidCommandStub, CancellationToken.None);

        await actual.Should().ThrowAsync<ArgumentNullException>();
    }
}