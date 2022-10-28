namespace Catalog.API.Features.CatalogItems;

public class Delete
{
    public record Command(Guid id) : IRequest<bool>;

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly ICatalogDbContext _db;
        private readonly IValidator<Command> _validator;

        public Handler(ICatalogDbContext context, IValidator<Command> validator)
        {
            _db = context ?? throw new ArgumentNullException(nameof(context));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(command);
            CatalogItem? deletedItem = await _db.FindOneAndDeleteAsync(command.id, cancellationToken);

            return deletedItem is not null;
        }
    }
}