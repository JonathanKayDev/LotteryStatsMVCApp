using Npgsql;

namespace LotteryStatsMVCApp.Services
{
    public class ConnectionService
    {
        #region Get Connection String
        public static string GetConnectionString(IConfiguration configuration)
        {
            //The default connection string will come from appSettings/secrets like usual
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            //It will be automatically overwritten if we are running on Heroku
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            return string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(databaseUrl);
        } 
        #endregion

        #region Build Connection String
        private static string BuildConnectionString(string databaseUrl)
        {
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };

            return builder.ToString();
        } 
        #endregion

    }
}
