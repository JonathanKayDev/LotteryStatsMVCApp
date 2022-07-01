using LotteryStatsMVCApp.Models;
using LotteryStatsMVCApp.Models.Enums;

namespace LotteryStatsMVCApp.Services
{
    public class WebFileProcessor
    {
        public List<DrawHistoryModel> ConvertToDrawHistoryModel(string game, List<string> lines)
        {
            List<DrawHistoryModel> output = new();

            // get main ball and bonus ball columns
            int[] ballCols = GetNumberOfBalls(game);
            int mbFirstCol = ballCols[0];
            int mbLastCol = ballCols[1];
            int bbFirstCol = ballCols[2];
            int bbLastCol = ballCols[3];

            foreach (string l in lines)
            {
                int bonusBallCount = 1;
                string mainBallNum = "";
                string bonusBallNum = "";

                string[] cols = l.Split(',');

                DrawHistoryModel d = new DrawHistoryModel();

                d.GameName = game;
                d.DrawDate = cols[0];

                // loop main balls
                for (int i = mbFirstCol; i <= mbLastCol; i++)
                {
                    mainBallNum = $"MainBall{i}";
                    d.GetType().GetProperty(mainBallNum).SetValue(d, int.Parse(cols[i]));
                }

                // loop bonus balls
                if (bbFirstCol != 0)
                {
                    for (int i = bbFirstCol; i <= bbLastCol; i++)
                    {
                        bonusBallNum = $"BonusBall{bonusBallCount}";
                        d.GetType().GetProperty(bonusBallNum).SetValue(d, int.Parse(cols[i])); // need to add column offset
                        bonusBallCount++;
                    } 
                }
                // TODO - Add Ballset, Machine and UKMillionaireMaker
                //d.UKMillionaireMaker = cols[8]; // TODO - capture multiple UKmillionairemaker? currently only the first is saved
                d.DrawNumber = int.Parse(cols.Last()); // Used last since this column not always the same due to potentially multiple UKmullionairmake

                output.Add(d);
            }

            return output;
        }

        private int[] GetNumberOfBalls(string game)
        {
            int mbFirstCol = 0;
            int mbLastCol = 0;
            int bbFirstCol = 0;
            int bbLastCol = 0;

            switch (game)
            {
                case nameof(Games.Euromillions):
                    mbFirstCol = 1;
                    mbLastCol = 5;
                    bbFirstCol = 6;
                    bbLastCol = 7;
                    break;
                case nameof(Games.EuromillionsHotpicks):
                    mbFirstCol = 1;
                    mbLastCol = 5;
                    break;
                case nameof(Games.Lotto):
                    mbFirstCol = 1;
                    mbLastCol = 6;
                    bbFirstCol = 7;
                    bbLastCol = 7;
                    break;
                case nameof(Games.LottoHotpicks):
                    mbFirstCol = 1;
                    mbLastCol = 6;
                    break;
                case nameof(Games.SetForLife):
                    mbFirstCol = 1;
                    mbLastCol = 5;
                    bbFirstCol = 6;
                    bbLastCol = 6;
                    break;
                case nameof(Games.Thunderball):
                    mbFirstCol = 1;
                    mbLastCol = 5;
                    bbFirstCol = 6;
                    bbLastCol = 6;
                    break;
                default:
                    break;
            }

            return  new int[] {mbFirstCol, mbLastCol, bbFirstCol, bbLastCol };
        }
    }
}
