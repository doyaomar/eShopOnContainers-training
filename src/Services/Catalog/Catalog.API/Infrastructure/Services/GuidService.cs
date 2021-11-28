namespace Catalog.API.Infrastructure.Services;

public class GuidService
{
    public Guid GetNewGuid() => Guid.NewGuid();
}