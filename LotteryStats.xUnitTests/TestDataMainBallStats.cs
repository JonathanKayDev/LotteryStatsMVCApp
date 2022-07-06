using LotteryStatsMVCApp.Models;
using LotteryStatsMVCApp.Models.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryStats.xUnitTests
{
    internal class TestDataMainBallStats : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            // EUROMILLIONS
            string game = nameof(Games.Euromillions);

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

            yield return new object[] { expected, game, drawHistory };

            // LOTTO

            game = nameof(Games.Lotto);

            expected = new();

            for (int i = 1; i <= 59; i++)
            {
                BallModel mainBall = new();
                mainBall.Number = i;
                mainBall.Frequency = 0;
                mainBall.Absence = 0;
                expected.Add(mainBall);
            }

            // set expected frequency
            for (int i = 20; i <= 25; i++)
            {
                expected[i].Frequency = 20;
            }

            // set expected absence
            for (int i = 0; i < 59; i++)
            {
                if (i < 20 || i > 25)
                {
                    expected[i].Absence = 20;
                }
            }

            // Draw data
            drawHistory = new();

            // Test 20 draw histories
            for (int i = 1; i <= 20; i++)
            {
                DrawHistoryModel draw = new();
                // set all main balls to 1
                draw.MainBall1 = 21;
                draw.MainBall2 = 22;
                draw.MainBall3 = 23;
                draw.MainBall4 = 24;
                draw.MainBall5 = 25;
                draw.MainBall6 = 26;
                //draw.DrawNumber = i;
                drawHistory.Add(draw);
            }

            yield return new object[] { expected, game, drawHistory };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
