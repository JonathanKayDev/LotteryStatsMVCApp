using FluentAssertions;
using LotteryStatsMVCApp.Models;
using LotteryStatsMVCApp.Models.Enums;
using LotteryStatsMVCApp.Services;
using System.Collections.Generic;
using Xunit;

namespace LotteryStats.xUnitTests
{
    public class StatsLogicTests
    {
        // Properties
        private readonly StatsLogic _sut; // sut means System Under Test

        // Constructor
        public StatsLogicTests()
        {
            _sut = new StatsLogic();
        }

        // Single test
        [Fact]
        public void TestNumberOfMainBalls_Euromillions()
        {
            int output = _sut.NumberOfMainBalls(nameof(Games.Euromillions));

            Assert.Equal(5, output);
        }

        // Inline tests
        [Theory]
        [InlineData(5, nameof(Games.Euromillions))]
        [InlineData(5, nameof(Games.EuromillionsHotpicks))]
        [InlineData(6, nameof(Games.Lotto))]
        [InlineData(6, nameof(Games.LottoHotpicks))]
        [InlineData(5, nameof(Games.SetForLife))]
        [InlineData(5, nameof(Games.Thunderball))]
        public void TestNumberOfMainBalls_Theory(int expected, string game)
        {
            int output = _sut.NumberOfMainBalls(game);

            Assert.Equal(expected, output);
        }

        // Class data test
        [Theory]
        [ClassData(typeof(TestDataMainBallStats))]
        public void TestCalcMainBallStats_Theory(List<BallModel> expected, string game, List<DrawHistoryModel> drawHistory)
        {
            List<BallModel> output = _sut.CalcMainBallStats(game, drawHistory);

            // Need to use BeEquivalentTo when comparing lists
            output.Should().BeEquivalentTo(expected);
        }

        // Member data test - from static member
        [Theory]
        [MemberData(nameof(TestData_MainBallStats_Euromillions))]
        public void TestCalcMainBallStats_Euromillions(List<BallModel> expected, List<DrawHistoryModel> drawHistory)
        {
            List<BallModel> output = _sut.CalcMainBallStats(nameof(Games.Euromillions), drawHistory);

            // Need to use BeEquivalentTo when comparing lists
            output.Should().BeEquivalentTo(expected);
        }

        
        public static IEnumerable<object[]> TestData_MainBallStats_Euromillions()
        {
            // Expected
            List<BallModel> expected = new();

            for (int i = 1; i <= 50; i++)
            {
                BallModel mainBall = new();
                mainBall.Number = i;
                mainBall.Frequency = 0;
                mainBall.Absence = 0;
                expected.Add(mainBall);
            }

            // set expected frequency
            expected[0].Frequency = 50;

            // set expected absence
            for (int i = 1; i < 50; i++)
            {
                expected[i].Absence = 10;
            }

            // Draw data
            List<DrawHistoryModel> drawHistory = new();

            // Test 10 draw histories
            for (int i = 1; i <= 10; i++)
            {
                DrawHistoryModel draw = new();
                // set all main balls to 1
                draw.MainBall1 = 1;
                draw.MainBall2 = 1;
                draw.MainBall3 = 1;
                draw.MainBall4 = 1;
                draw.MainBall5 = 1;
                //draw.DrawNumber = i;
                drawHistory.Add(draw);
            }

            yield return new object[] { expected, drawHistory };
        }

    }
}