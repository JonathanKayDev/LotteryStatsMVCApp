namespace LotteryStatsMVCApp.Models
{
    public class DrawHistoryModel
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public string DrawDate { get; set; }
        public int? MainBall1 { get; set; }
        public int? MainBall2 { get; set; }
        public int? MainBall3 { get; set; }
        public int? MainBall4 { get; set; }
        public int? MainBall5 { get; set; }
        public int? MainBall6 { get; set; }
        public int? BonusBall1 { get; set; }
        public int? BonusBall2 { get; set; }
        public string? BallSet { get; set; }
        public string? Machine { get; set; }
        public int DrawNumber { get; set; }
        public string? UKMillionaireMaker { get; set; }
    }
}
