using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GameShop.Contracts.Entities;
using GameShop.Website.Services.GameShop.Interfaces;

namespace GameShop.Website.Controllers
{
    public class HomeController : Controller
    {
        private IGameShopApi _gameShopApi;

        public HomeController(IGameShopApi gameShopApi)
        {
            _gameShopApi = gameShopApi;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _gameShopApi.Products.GetAllAdsAsync();

            return View(products);
        }

        public async Task<IActionResult> Search(string id)
        {
            var products = await _gameShopApi.Products.FindAdsByTitleAsync(id);

            return View(products);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Test()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
