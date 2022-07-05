namespace LotteryStatsMVCApp.Models.Settings
{
    public class AppSettings
    {
        public DefaultCredentials? DefaultCredentials { get; set; }
    }

    public class DefaultCredentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
