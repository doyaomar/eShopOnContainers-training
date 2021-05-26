using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Infrastructure
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> context) : base(context)
        { }

        public DbSet<CatalogItem> CatalogItems { get; set; }

        public DbSet<CatalogBrand> CatalogBrands { get; set; }

        public DbSet<CatalogType> catalogTypes { get; set; }
    }
}