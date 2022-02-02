namespace Data.Configurations;

public class DataAccessSettings
{
    public string ConnectionString { get; set; }
    public string Server { get; set; }
}

public static class DataAccessServer
{
    public const string SqlServer = "SqlServer";
    public const string InMemory = "InMemory";
}