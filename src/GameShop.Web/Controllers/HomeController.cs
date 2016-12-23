using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using GameShop.Web.Options;
using Microsoft.Extensions.Options;

namespace GameShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private GameShopApiOptions _apiOptions;

        public HomeController(IOptions<GameShopApiOptions> apiOptions)
        {
            _apiOptions = apiOptions.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        
        public async Task<IActionResult> Contact()
        {
            // Get bearer from cooking.
            //string bearerToken = Request.Cookies[GameShopConstants.];

            HttpClient client = new HttpClient();
            // Set bearer token.
            //client.SetBearerToken(bearerToken);

            using (var response = await client.GetAsync("http://localhost:5001/test/admin"))
            {
                var responseText = await response.Content.ReadAsStringAsync();

                ViewData["Message"] = responseText;
            }

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
