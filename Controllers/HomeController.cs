﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VierGewinnt.Models;

namespace VierGewinnt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Leaderboard()
        {
            return View();
        }

        public IActionResult MatchHistory()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Homepage()
        {
            return View();
        }

    }
}