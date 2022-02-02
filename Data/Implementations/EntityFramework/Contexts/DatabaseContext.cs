using Core.Utilities.IoC;
using Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Entities;

namespace Data.Implementations.EntityFramework.Contexts;

public class DatabaseContext : DbContext
{

    public DatabaseContext() : base()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = ConfigurationTool.Get();
            var dataAccessSettings = configuration.GetSection("DataAccessSettings").Get<DataAccessSettings>();
            optionsBuilder.DbContextBuild(dataAccessSettings);
            
        }
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<User> Users { get; set; }
}