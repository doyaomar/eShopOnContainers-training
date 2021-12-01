namespace Catalog.API.Models;

public abstract class Entity<T>
{
    public T Id { get; protected set; } = default!;

    public virtual void SetId(T id) => Id = id;
}
