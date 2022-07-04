using LotteryStatsMVCApp.Models;
using LotteryStatsMVCApp.Models.Enums;

namespace LotteryStatsMVCApp.Services
{
    public class StatsLogic
    {
        #region Calc Bonus Ball Stats
        public List<BallModel> CalcBonusBallStats(string game, List<DrawHistoryModel> drawHistory)
        {
            // return null if game has no bonus balls
            if (game == nameof(Games.EuromillionsHotpicks) || game == nameof(Games.EuromillionsHotpicks))
            {
                return null;
            }

            List<BallModel> stats = new();
            int bonusBallRange = BonusBallRange(game);

            // generate bonus ball list
            for (int i = 1; i <= bonusBallRange; i++)
            {
                BallModel bonusBall = new();
                bonusBall.Number = i;
                bonusBall.Frequency = 0;
                bonusBall.Absence = 0;
                stats.Add(bonusBall);
            }

            // find stats
            stats = FindBonusBallCommonNumbers(stats, drawHistory);
            stats = FindBonusBallAbsentNumbers(stats, drawHistory);

            return stats;
        } 
        #endregion

        #region Calc Main Ball Stats
        public List<BallModel> CalcMainBallStats(string game, List<DrawHistoryModel> drawHistory)
        {
            List<BallModel> stats = new();
            int mainBallRange = MainBallRange(game);

            // generate main ball list
            for (int i = 1; i <= mainBallRange; i++)
            {
                BallModel mainBall = new();
                mainBall.Number = i;
                mainBall.Frequency = 0;
                mainBall.Absence = 0;
                stats.Add(mainBall);
            }

            // find stats
            stats = FindMainBallCommonNumbers(stats, drawHistory);
            stats = FindMainBallAbsentNumbers(stats, drawHistory);

            return stats;
        } 
        #endregion

        #region Find Bonus Ball Absent Numbers
        private List<BallModel> FindBonusBallAbsentNumbers(List<BallModel> stats, List<DrawHistoryModel> drawHistory)
        {
            // tall absent numbers
            foreach (DrawHistoryModel d in drawHistory)
            {
                // set all balls to +1 each game
                foreach (BallModel b in stats)
                {
                    b.Absence += 1;
                }

                // if ball is in draw, then reset the absence count
                int num = 0;
                for (int i = 1; i <= 2; i++)
                {
                    if (d.GetType().GetProperty($"BonusBall{i}").GetValue(d) != null)
                    {
                        num = (int)d.GetType().GetProperty($"BonusBall{i}").GetValue(d);
                        stats[num - 1].Absence = 0;
                    }
                }
            }

            return stats;
        } 
        #endregion

        #region Find Bonus Ball Common Numbers
        private List<BallModel> FindBonusBallCommonNumbers(List<BallModel> stats, List<DrawHistoryModel> drawHistory)
        {
            // tally common numbers
            foreach (DrawHistoryModel d in drawHistory)
            {
                int num = 0;
                for (int i = 1; i <= 2; i++)
                {
                    if (d.GetType().GetProperty($"BonusBall{i}").GetValue(d) != null)
                    {
                        num = (int)d.GetType().GetProperty($"BonusBall{i}").GetValue(d);
                        stats[num - 1].Frequency += 1;
                    }
                }
            }

            return stats;
        } 
        #endregion

        #region Find Main Ball Absent Numbers
        private List<BallModel> FindMainBallAbsentNumbers(List<BallModel> stats, List<DrawHistoryModel> drawHistory)
        {
            // tall absent numbers
            foreach (DrawHistoryModel d in drawHistory)
            {
                // set all balls to +1 each game
                foreach (BallModel b in stats)
                {
                    b.Absence += 1;
                }

                // if ball is in draw, then reset the absence count
                int num = 0;
                for (int i = 1; i <= 6; i++)
                {
                    if (d.GetType().GetProperty($"MainBall{i}").GetValue(d) != null)
                    {
                        num = (int)d.GetType().GetProperty($"MainBall{i}").GetValue(d);
                        stats[num - 1].Absence = 0;
                    }
                }
            }

            return stats;
        } 
        #endregion

        #region Find Main Ball Common Numbers
        private List<BallModel> FindMainBallCommonNumbers(List<BallModel> stats, List<DrawHistoryModel> drawHistory)
        {
            // tally common numbers
            foreach (DrawHistoryModel d in drawHistory)
            {
                int num = 0;
                for (int i = 1; i <= 6; i++)
                {
                    if (d.GetType().GetProperty($"MainBall{i}").GetValue(d) != null)
                    {
                        num = (int)d.GetType().GetProperty($"MainBall{i}").GetValue(d);
                        stats[num - 1].Frequency += 1;
                    }
                }
            }

            return stats;
        }
        #endregion

        #region Bonus Ball Range
        private int BonusBallRange(string game)
        {
            int bonusBallRange = 0;

            switch (game)
            {
                case nameof(Games.Euromillions):
                    bonusBallRange = 12;
                    break;
                case nameof(Games.Lotto):
                    bonusBallRange = 59;
                    break;
                case nameof(Games.SetForLife):
                    bonusBallRange = 10;
                    break;
                case nameof(Games.Thunderball):
                    bonusBallRange = 14;
                    break;
                default:
                    break;
            }

            return bonusBallRange;
        }
        #endregion

        #region Main Ball Range
        private int MainBallRange(string game)
        {
            int mainBallRange = 0;

            switch (game)
            {
                case nameof(Games.Euromillions):
                    mainBallRange = 50;
                    break;
                case nameof(Games.EuromillionsHotpicks):
                    mainBallRange = 50;
                    break;
                case nameof(Games.Lotto):
                    mainBallRange = 59;
                    break;
                case nameof(Games.LottoHotpicks):
                    mainBallRange = 59;
                    break;
                case nameof(Games.SetForLife):
                    mainBallRange = 47;
                    break;
                case nameof(Games.Thunderball):
                    mainBallRange = 39;
                    break;
                default:
                    break;
            }

            return mainBallRange;
        }
        #endregion

        #region Number of Bonus Balls
        public int NumberOfBonusBalls(string game)
        {
            int num = 0;

            switch (game)
            {
                case nameof(Games.Euromillions):
                    num = 2;
                    break;
                case nameof(Games.Lotto):
                    num = 1;
                    break;
                case nameof(Games.SetForLife):
                    num = 1;
                    break;
                case nameof(Games.Thunderball):
                    num = 1;
                    break;
                default:
                    break;
            }

            return num;
        }
        #endregion

        #region Number of Main Balls
        public int NumberOfMainBalls(string game)
        {
            int num = 0;

            switch (game)
            {
                case nameof(Games.Euromillions):
                    num = 5;
                    break;
                case nameof(Games.EuromillionsHotpicks):
                    num = 5;
                    break;
                case nameof(Games.Lotto):
                    num = 6;
                    break;
                case nameof(Games.LottoHotpicks):
                    num = 6;
                    break;
                case nameof(Games.SetForLife):
                    num = 5;
                    break;
                case nameof(Games.Thunderball):
                    num = 5;
                    break;
                default:
                    break;
            }

            return num;
        } 
        #endregion

    }
}
