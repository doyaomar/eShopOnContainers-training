namespace Catalog.API.Features.CatalogItems;

public class Delete
{
    public record Command(Guid id) : IRequest<bool>;

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly CatalogDbContext _context;

        public Handler(CatalogDbContext context)
        => _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                return false; // replace by exception ??
            }

            CatalogItem deletedItem = await _context
            .CatalogItems
            .FindOneAndDeleteAsync(x => x.Id == request.id, null, cancellationToken);

            return deletedItem is not null;
        }
    }
}