using Microsoft.EntityFrameworkCore;
using WarehouseInventory.Api.Models;

namespace WarehouseInventory.Api.Data;

public class WarehouseDbContext : DbContext
{
    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
}
