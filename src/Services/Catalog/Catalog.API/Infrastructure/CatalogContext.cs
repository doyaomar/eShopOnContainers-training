using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;
using Catalog.API.Infrastructure.EntityConfigurations;

namespace Catalog.API.Infrastructure
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> context) : base(context)
        { }

        public DbSet<CatalogItem> CatalogItems { get; set; } = default!;

        public DbSet<CatalogBrand> CatalogBrands { get; set; } = default!;

        public DbSet<CatalogType> catalogTypes { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogTypeEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogBrandEntityTypeConfiguration());
        }
    }
}