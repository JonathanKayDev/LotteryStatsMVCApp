using LotteryStatsMVCApp.Data;
using LotteryStatsMVCApp.Models;
using LotteryStatsMVCApp.Models.Enums;
using LotteryStatsMVCApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LotteryStatsMVCApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UpdateGame(string game)
        {
            // get draw results from web
            WebConnector wc = new();
            List<DrawHistoryModel> webGames = wc.WebDrawHistory(game);

            // reverse web file so we add data in correct order
            webGames.Reverse();

            // get draw results from Db
            var savedGames = await _context.DrawHistory
                                .Where(d => d.GameName == game)
                                .OrderBy(d => d.DrawNumber)
                                .ToListAsync();

            // compare
            foreach (DrawHistoryModel d in webGames)
            {
                // check if web data is stored in Db
                if (savedGames.FirstOrDefault(x => x.DrawNumber == d.DrawNumber) == null)
                {
                    _context.Add(d);
                }

            }

            await _context.SaveChangesAsync();

            // test code
            //WebConnector wc = new();
            //List<DrawHistoryModel> history = new();
            //history = wc.WebDrawHistory(nameof(Games.Lotto));

            return RedirectToAction("Index");
        }
    }
}
