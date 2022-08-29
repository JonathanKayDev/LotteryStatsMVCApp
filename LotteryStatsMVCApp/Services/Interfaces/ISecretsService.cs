namespace LotteryStatsMVCApp.Services.Interfaces
{
    public interface ISecretsService
    {
        public string? GetDefaultEmail();
        public string? GetDefaultPassword();
    }
}
