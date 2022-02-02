using System.Reflection;
using Business.Configurations;
using Core.Utilities.IoC;
using Data.Configurations;
using Data.Implementations.EntityFramework.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace Business.Test;

public class Startup
{
    public Startup(IHostingEnvironment env)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

        Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var configuration = ConfigurationTool.Get();

        DataAccessConfiguration.ConfigureService(services, configuration);

        AutoMapperConfiguration.ConfigureService(services);

        BusinessConfiguration.ConfigureService(services);

        var serviceProvider = services.BuildServiceProvider();
        var context = serviceProvider.GetRequiredService<DatabaseContext>();
        context.RemoveRange(context.Discounts);
        context.RemoveRange(context.Users);
        context.RemoveRange(context.Orders);
        context.RemoveRange(context.OrderItems);
        context.SaveChanges();
    }

    public void Configure(IApplicationBuilder app)
    {
    }
}