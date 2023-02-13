namespace Catalog.UnitTests.Features.CatalogItems;

public class CreateTests
{
    private readonly Mock<ICatalogDbContext> _dbStub;
    private readonly Mock<IMapper> _mapperStub;
    private readonly Mock<IGuidService> _guidServiceStub;
    private readonly Mock<IFileService> _fileServiceStub;
    private readonly Create.Handler _handler;
    private readonly IValidator<Create.Command> _validator;

    public CreateTests()
    {
        _dbStub = new();
        _mapperStub = new();
        _guidServiceStub = new();
        _fileServiceStub = new();
        _validator = new CreateValidator();
        _handler = new(_dbStub.Object, _mapperStub.Object, _guidServiceStub.Object, _fileServiceStub.Object, _validator);
    }

    [Fact]
    public async Task Handle_WhenCommandIsValid_ThenReturnsNewId()
    {
        var validRequestStub = CatalogItemFakes.GetCreateCommandFake();
        var validProductIdMock = Guid.NewGuid();
        var catalogItemStub = CatalogItemFakes.GetCatalogItemFake();
        _mapperStub.Setup(mapper => mapper.Map<CatalogItem>(validRequestStub)).Returns(catalogItemStub);
        _guidServiceStub.Setup(svc => svc.GetNewGuid()).Returns(validProductIdMock);
        _fileServiceStub.Setup(svc => svc.PathGetExtension(It.IsAny<string>())).Returns(".png");
        _dbStub.Setup(db => db.InsertOneAsync(catalogItemStub, CancellationToken.None)).ReturnsAsync(validProductIdMock);

        var actual = await _handler.Handle(validRequestStub, CancellationToken.None);

        actual.Should().Be(validProductIdMock);
    }

    [Fact]
    public async Task Handle_WhenCommandIsNotValidValid_ThenThrowsValidationException()
    {
        var validRequestStub = CatalogItemFakes.GetCreateCommandInvalidFake();

        Func<Task> actual = async () => await _handler.Handle(validRequestStub, CancellationToken.None);

        await actual.Should().ThrowAsync<ValidationException>();
    }
}
