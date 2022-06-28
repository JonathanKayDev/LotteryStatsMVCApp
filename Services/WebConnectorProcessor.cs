using System.Net;

namespace LotteryStatsMVCApp.Services
{
    public static class WebConnectorProcessor
    {
        public static string webPath = "https://www.national-lottery.co.uk/results/";

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
