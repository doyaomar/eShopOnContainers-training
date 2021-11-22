namespace Catalog.API.SeedWork;

public class GuidProvider : IGuidProvider
{
    public Guid GetNewGuid() => Guid.NewGuid();
}