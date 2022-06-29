using System.Net;

namespace LotteryStatsMVCApp.Services
{
    public static class WebConnectorProcessor
    {
        // Root path for web files
        public static string webPath = "https://www.national-lottery.co.uk/results/";

        // Web files
        public const string EuromillionsWeb = "euromillions/draw-history/csv/";
        public const string EuromillionsHotpicksWeb = "euromillions-hotpicks/draw-history/csv/";
        public const string LottoWeb = "lotto/draw-history/csv/";
        public const string LottoHotpicksWeb = "lotto-hotpicks/draw-history/csv/";
        public const string SetforlifeWeb = "set-for-life/draw-history/csv/";
        public const string ThunderballWeb = "thunderball/draw-history/csv/";

        public static string FullWebPath(string webFile)
        {
            return $"{webPath}{ webFile }";
        }

        public static List<string> LoadWeb(this string url)
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
