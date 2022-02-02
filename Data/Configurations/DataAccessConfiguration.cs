using System.Data.SQLite;
using Data.Implementations.EntityFramework;
using Data.Implementations.EntityFramework.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Configurations;

public static class DataAccessConfiguration
{
    public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
    {
        var dataAccessSettings = configuration.GetSection("DataAccessSettings").Get<DataAccessSettings>();

        services.AddDbContext<DatabaseContext>(options => { options.DbContextBuild(dataAccessSettings); });

        services.AddTransient<IOrderDal, EfOrderDal>();
        services.AddTransient<IOrderItemDal, EfOrderItemDal>();
        services.AddTransient<IUserDal, EfUserDal>();
        services.AddTransient<IDiscountDal, EfDiscountDal>();
    }

    public static void DbContextBuild(this DbContextOptionsBuilder options, DataAccessSettings settings)
    {
        switch (settings.Server)
        {
            case DataAccessServer.SqlServer:
                options.UseSqlServer(settings.ConnectionString);
                options.EnableSensitiveDataLogging();
                break;
            case DataAccessServer.InMemory:
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                options.EnableSensitiveDataLogging();
                break;
            default:
                throw new NotImplementedException();
        }
    }
}