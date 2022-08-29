using LotteryStatsMVCApp.Models.Settings;
using LotteryStatsMVCApp.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace LotteryStatsMVCApp.Services
{
    public class SecretsService : ISecretsService
    {
        private readonly AppSettings _appSettings;

        public SecretsService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string? GetDefaultEmail()
        {
            var ev = Environment.GetEnvironmentVariable("DefaultEmail");

            return string.IsNullOrEmpty(ev) ? _appSettings.DefaultCredentials.Email : ev;
        }

        public string? GetDefaultPassword()
        {
            var ev = Environment.GetEnvironmentVariable("DefaultPassword");

            return string.IsNullOrEmpty(ev) ? _appSettings.DefaultCredentials.Password : ev;
        }
    }
}
