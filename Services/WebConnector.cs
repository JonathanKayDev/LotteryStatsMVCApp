using LotteryStatsMVCApp.Models;
using LotteryStatsMVCApp.Models.Enums;

namespace LotteryStatsMVCApp.Services
{
    public class WebConnector
    {
        public List<DrawHistoryModel> WebEuromillionsDrawHistory()
        {
            WebFileProcessor webFileProcessor = new();
            List<string> lines = WebConnectorProcessor.FullWebPath(WebConnectorProcessor.EuromillionsWeb).LoadWeb();
            List<DrawHistoryModel> output = webFileProcessor.ConvertToDrawHistoryModel(nameof(Games.Euromillions), lines);//WebConnectorProcessor.FullWebPath(WebConnectorProcessor.EuromillionsWeb).LoadWeb();
            return output;
        }
    }
}
