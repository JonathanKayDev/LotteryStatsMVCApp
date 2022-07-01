using LotteryStatsMVCApp.Models;
using LotteryStatsMVCApp.Models.Enums;
using System.Net;

namespace LotteryStatsMVCApp.Services
{
    public class WebConnector
    {

        // Root path for web files
        private const string webPath = "https://www.national-lottery.co.uk/results/";

        // Web files
        private const string EuromillionsWeb = "euromillions/draw-history/csv/";
        private const string EuromillionsHotpicksWeb = "euromillions-hotpicks/draw-history/csv/";
        private const string LottoWeb = "lotto/draw-history/csv/";
        private const string LottoHotpicksWeb = "lotto-hotpicks/draw-history/csv/";
        private const string SetforlifeWeb = "set-for-life/draw-history/csv/";
        private const string ThunderballWeb = "thunderball/draw-history/csv/";

        public List<DrawHistoryModel> WebDrawHistory(string game)
        {
            WebFileProcessor webFileProcessor = new();
            List<string> lines = LoadWeb(FullWebPath(game));
            List<DrawHistoryModel> output = webFileProcessor.ConvertToDrawHistoryModel(game, lines);
            return output;
        }

        private string FullWebPath(string game)
        {
            string webFile = "";

            switch (game)
            {
                case nameof(Games.Euromillions):
                    webFile = EuromillionsWeb;
                    break;
                case nameof(Games.EuromillionsHotpicks):
                    webFile = EuromillionsHotpicksWeb;
                    break;
                case nameof(Games.Lotto):
                    webFile = LottoWeb;
                    break;
                case nameof(Games.LottoHotpicks):
                    webFile = LottoHotpicksWeb;
                    break;
                case nameof(Games.SetForLife):
                    webFile = SetforlifeWeb;
                    break;
                case nameof(Games.Thunderball):
                    webFile = ThunderballWeb;
                    break;
                default:
                    break;
            }

            return $"{webPath}{ webFile }";
        }

        private List<string> LoadWeb(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());

            List<string> webCsv = new List<string>();

            while (!sr.EndOfStream)
            {
                webCsv.Add(sr.ReadLine());
            }

            webCsv.RemoveAt(0); // remove header row

            return webCsv;

        }


    }
}
