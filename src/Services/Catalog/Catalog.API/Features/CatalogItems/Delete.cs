namespace Catalog.API.Features.CatalogItems;

public class Delete
{
    public record Command(Guid id) : IRequest<bool>;

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly ICatalogDbContext _db;

        public Handler(ICatalogDbContext context)
        => _db = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));
            CatalogItem deletedItem = await _db
            .CatalogItems
            .FindOneAndDeleteAsync(x => x.Id == request.id, null, cancellationToken);

            return deletedItem is not null;
        }
    }
}