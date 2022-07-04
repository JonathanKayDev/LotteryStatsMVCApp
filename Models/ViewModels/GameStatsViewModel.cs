namespace LotteryStatsMVCApp.Models.ViewModels
{
    public class GameStatsViewModel
    {
        public string GameName { get; set; }
        public int NumberOfMainBalls { get; set; }
        public int NumberOfBonusBalls { get; set; }
        public List<BallModel> MainBallStats { get; set; }
        public List<BallModel> BonusBallStats { get; set; }
    }
}
