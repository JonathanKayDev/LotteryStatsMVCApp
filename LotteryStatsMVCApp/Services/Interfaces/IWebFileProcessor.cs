using LotteryStatsMVCApp.Models;

namespace LotteryStatsMVCApp.Services.Interfaces
{
    public interface IWebFileProcessor
    {
        public List<DrawHistoryModel> ConvertToDrawHistoryModel(string game, List<string> lines);
    }
}
