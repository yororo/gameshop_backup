using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GameShop.Interface.Entities;
using GameShop.Web.Services.GameShopApis.Interfaces;

namespace GameShop.Web.Controllers
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
            var products = await _gameShopApi.Products.GetAllProductsAsync();

            return View(products);
        }

        public async Task<IActionResult> Search(string id)
        {
            var products = await _gameShopApi.Products.FindProductsByTitleAsync(id);

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
