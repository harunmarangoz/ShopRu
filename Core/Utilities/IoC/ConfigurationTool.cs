using Microsoft.Extensions.Configuration;

namespace Core.Utilities.IoC;

public static class ConfigurationTool
{
    public static IConfiguration Get()
    {
        ConfigurationBuilder configurationBuilder = new();
        configurationBuilder.AddJsonFile("appsettings.json", false, true);
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        configurationBuilder.AddJsonFile($"appsettings.{env}.json", true);
        IConfigurationRoot configuration = configurationBuilder.Build();
        return configuration;
    }
}