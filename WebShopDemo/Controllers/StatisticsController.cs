﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopDemo.Abstraction;
using WebShopDemo.Models.Statistics;

namespace WebShopDemo.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService statisticsService;
        public StatisticsController(IStatisticsService statisticsService)
        { this.statisticsService = statisticsService; }
        public IActionResult Index()
        {
            StatisticsVM statistics = new StatisticsVM();
            statistics.CountClients = statisticsService.CountClients();
            statistics.CountProducts = statisticsService.CountProducts();
            statistics.CountOrders = statisticsService.CountOrders();
            statistics.SumOrders = (int)statisticsService.SumOrders();

            return View(statistics);
        }
    }
}
