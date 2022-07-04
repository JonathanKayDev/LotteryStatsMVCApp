using LotteryStatsMVCApp.Data;
using LotteryStatsMVCApp.Models;
using LotteryStatsMVCApp.Models.Enums;
using LotteryStatsMVCApp.Models.ViewModels;
using LotteryStatsMVCApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LotteryStatsMVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // test code
            //WebConnector wc = new();
            //List<DrawHistoryModel> history = new();
            //history = wc.WebDrawHistory(nameof(Games.Lotto));

            return View();
        }

        public async Task<IActionResult> GameStats(string game)
        {
            // get draw results from Db
            List<DrawHistoryModel> savedGames = await _context.DrawHistory
                                .Where(d => d.GameName == game)
                                .OrderBy(d => d.DrawNumber)
                                .ToListAsync();

            // get stats
            StatsLogic sl = new();
            GameStatsViewModel model = new();
            model.GameName = game;
            model.MainBallStats = sl.CalcMainBallStats(game, savedGames);
            model.BonusBallStats = sl.CalcBonusBallStats(game, savedGames);
            model.NumberOfMainBalls = sl.NumberOfMainBalls(game);
            model.NumberOfBonusBalls = sl.NumberOfBonusBalls(game);

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}