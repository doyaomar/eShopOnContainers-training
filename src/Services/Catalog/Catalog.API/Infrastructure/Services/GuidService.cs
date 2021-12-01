namespace Catalog.API.Infrastructure.Services;

public class GuidService : IGuidService
{
    public Guid GetNewGuid() => Guid.NewGuid();
}