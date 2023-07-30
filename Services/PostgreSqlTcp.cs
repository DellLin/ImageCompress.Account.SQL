using Npgsql;

public class PostgreSqlTcp
    {
        public static NpgsqlConnectionStringBuilder NewPostgreSqlTCPConnectionString()
        {
            System.Console.WriteLine(Environment.GetEnvironmentVariable("INSTANCE_CONNECTION_NAME"));
            System.Console.WriteLine(Environment.GetEnvironmentVariable("INSTANCE_HOST"));
            System.Console.WriteLine(Environment.GetEnvironmentVariable("DB_USER"));
            System.Console.WriteLine(Environment.GetEnvironmentVariable("DB_PASS"));
            System.Console.WriteLine(Environment.GetEnvironmentVariable("DB_NAME"));
            // Equivalent connection string:
            // "Uid=<DB_USER>;Pwd=<DB_PASS>;Host=<INSTANCE_HOST>;Database=<DB_NAME>;"
            var connectionString = new NpgsqlConnectionStringBuilder()
            {
                // Note: Saving credentials in environment variables is convenient, but not
                // secure - consider a more secure solution such as
                // Cloud Secret Manager (https://cloud.google.com/secret-manager) to help
                // keep secrets safe.
                // Host = Environment.GetEnvironmentVariable("INSTANCE_CONNECTION_NAME"),     // e.g. '127.0.0.1'
                Host = Environment.GetEnvironmentVariable("INSTANCE_HOST"),
                // Set Host to 'cloudsql' when deploying to App Engine Flexible environment
                Username = Environment.GetEnvironmentVariable("DB_USER"), // e.g. 'my-db-user'
                Password = Environment.GetEnvironmentVariable("DB_PASS"), // e.g. 'my-db-password'
                Database = Environment.GetEnvironmentVariable("DB_NAME"), // e.g. 'my-database'
                // RootCertificate = Environment.GetEnvironmentVariable("DB_SERVER_CA"),
                // SslCertificate = Environment.GetEnvironmentVariable("DB_CLIENT_CERT"),
                // SslPassword = Environment.GetEnvironmentVariable("DB_CLIENT_KEY"),
                // The Cloud SQL proxy provides encryption between the proxy and instance.
                // TrustServerCertificate = false,
                SslMode = SslMode.Disable,
                
            };
             // [END cloud_sql_postgres_dotnet_ado_connect_tcp]
            // For deployments that connect directly to a Cloud SQL instance without
            // using the Cloud SQL Proxy, configuring SSL certificates will ensure the
            // connection is encrypted.
            // if (Environment.GetEnvironmentVariable("DB_CERT") != null)
            // {
            //     connectionString.SslMode = SslMode.VerifyCA;
            //     connectionString.SslCertificate =
            //         Environment.GetEnvironmentVariable("/Users/delllin/Projects/GCP/Cert/client.pfx"); // e.g. 'certs/client.pfx'
            // }
            connectionString.Pooling = true;
            // Specify additional properties here.
            return connectionString;
        }
    }
