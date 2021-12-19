namespace Catalog.UnitTests.Features.CatalogItems;

public class GetAllTests
{
    readonly Mock<ICatalogDbContext> _dbStub;
    readonly Mock<IMapper> _mapperStub;
    readonly GetAll.Handler _handler;

    public GetAllTests()
    {
        _mapperStub = new();
        _dbStub = new();
        _handler = new(_dbStub.Object, _mapperStub.Object);
    }

//     [Fact]
//     public async Task Handle_WhenQueryIsValid_ThenReturnsPaginatedDto()
//     {
//         var firstIdStub = Guid.NewGuid();
//         var secondIdStub = Guid.NewGuid();
//         var t = string.Join()
//         var validQueryStub = CatalogItemFakes.GetGetAllQueryFake(firstIdStub, secondIdStub);
//         var itemsMock = CatalogItemFakes.GetCatalogItemsFake<CatalogItem>();
//         var paginatedDtoStub = CatalogItemFakes.GetPaginatedDtoFake(itemsMock);
//         var t = ()
//         _dbStub.Setup(db => db.FindAllAsync(It.IsAny<IEnumerable<Guid>>(),It.IsAny<int>(),It.IsAny<int>(), CancellationToken.None));
//         _mapperStub.Setup(mapper => mapper.Map<CatalogItemDto>(catalogItemStub)).Returns(catalogItemDtoMock);

//         var actual = await _handler.Handle(validQueryStub, CancellationToken.None);

//         actual.Should().NotBeNull();
//         actual!.Id.Should().Be(catalogItemDtoMock.Id);
//         actual!.Name.Should().Be(catalogItemDtoMock.Name);
//     }
}