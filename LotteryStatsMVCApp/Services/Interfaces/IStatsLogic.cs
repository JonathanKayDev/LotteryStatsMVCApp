using LotteryStatsMVCApp.Models;

namespace LotteryStatsMVCApp.Services.Interfaces
{
    public interface IStatsLogic
    {
        public List<BallModel> CalcBonusBallStats(string game, List<DrawHistoryModel> drawHistory);
        public List<BallModel> CalcMainBallStats(string game, List<DrawHistoryModel> drawHistory);
        public int NumberOfBonusBalls(string game);
        public int NumberOfMainBalls(string game);
    }
}
