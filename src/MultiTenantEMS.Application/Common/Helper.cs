
namespace MultiTenantEMS.Application.Common
{
    public static class Helper
    {
        public static string DatabaseConnectionString
        {
            get
            {
                var server = Environment.GetEnvironmentVariable("DATABASE_SERVER");
                var port = Environment.GetEnvironmentVariable("DATABASE_PORT");
                var database = Environment.GetEnvironmentVariable("DATABASE_DATABASENAME");
                var user = Environment.GetEnvironmentVariable("DATABASE_USER");
                var password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");

                var connectionString =
                    $"Host={server};" +
                    $"Port={port};" +
                    $"Database={database};" +
                    $"Username={user};" +
                    $"Password={password};";

                return connectionString;
            }
        }
    }
}
