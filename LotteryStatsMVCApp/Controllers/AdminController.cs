﻿using LotteryStatsMVCApp.Data;
using LotteryStatsMVCApp.Models;
using LotteryStatsMVCApp.Models.Enums;
using LotteryStatsMVCApp.Services;
using LotteryStatsMVCApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LotteryStatsMVCApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebConnector _webConnector;

        public AdminController(ApplicationDbContext context, IWebConnector webConnector)
        {
            _context = context;
            _webConnector = webConnector;
        }

        // GET: AdminController
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGame(string game)
        {
            // get draw results from web
            //WebConnector wc = new();
            List<DrawHistoryModel> webGames = _webConnector.WebDrawHistory(game);
            // reverse web file so we add data in correct order
            webGames.Reverse();

            // get draw results from Db
            var savedGames = await _context.DrawHistory
                                .Where(d => d.GameName == game)
                                .OrderBy(d => d.DrawNumber)
                                .ToListAsync();

            // compare
            int newDataCounter = 0;
            foreach (DrawHistoryModel d in webGames)
            {
                // check if web data is stored in Db, if not then save
                if (savedGames.FirstOrDefault(x => x.DrawNumber == d.DrawNumber) == null)
                {
                    _context.Add(d);
                    newDataCounter++;
                }

            }

            await _context.SaveChangesAsync();

            TempData["message"] = $"{newDataCounter} game(s) added.";

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAllGames()
        {
            foreach (string game in Enum.GetNames(typeof(Games)))
            {
                // get draw results from web
                //WebConnector wc = new();
                List<DrawHistoryModel> webGames = _webConnector.WebDrawHistory(game);
                // reverse web file so we add data in correct order
                webGames.Reverse();

                // get draw results from Db
                List<DrawHistoryModel> savedGames = await _context.DrawHistory
                                    .Where(d => d.GameName == game)
                                    .OrderBy(d => d.DrawNumber)
                                    .ToListAsync();

                // compare
                int newDataCounter = 0;
                foreach (DrawHistoryModel d in webGames)
                {
                    // check if web data is stored in Db, if not then save
                    if (savedGames.FirstOrDefault(x => x.DrawNumber == d.DrawNumber) == null)
                    {
                        _context.Add(d);
                        newDataCounter++;
                    }

                }

                TempData["message"] += $"{game}: {newDataCounter} game(s) added. \n";

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
