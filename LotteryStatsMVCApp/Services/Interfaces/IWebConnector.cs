using LotteryStatsMVCApp.Models;

namespace LotteryStatsMVCApp.Services.Interfaces
{
    public interface IWebConnector
    {
        public List<DrawHistoryModel> WebDrawHistory(string game);
    }
}
