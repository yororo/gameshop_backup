using GameShop.Web.Services.GameShopApis.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using GameShop.Web.Options;
using Microsoft.Extensions.Options;
using GameShop.Contracts.Entities;

namespace GameShop.Web.Services.GameShopApis
{
    public class GameShopProductsApi : IGameShopProductsApi
    {
        private IOptions<GameShopApiOptions> _apiOptions;
        private ILogger _logger;

        public GameShopProductsApi(IOptions<GameShopApiOptions> apiOptions,ILoggerFactory loggerFactory)
        {
            _apiOptions = apiOptions;
            _logger = loggerFactory.CreateLogger<IGameShopProductsApi>();    
        }

        public async Task<IEnumerable<Ad>> GetAllProductsAsync()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    //Get URL from settings.
                    var url = _apiOptions.Value.Url;
                    client.BaseAddress = new Uri(url);

                    //Get request.
                    var response = await client.GetAsync("/api/products");

                    //Ensure that status code is successful.
                    response.EnsureSuccessStatusCode();

                    //Get Json string.
                    var responseString = await response.Content.ReadAsStringAsync();

                    //Deserialize Json string.
                    return JsonConvert.DeserializeObject<IEnumerable<Ad>>(responseString);
                }
                catch(HttpRequestException exception)
                {
                    _logger.LogCritical("API call resulted in an error: {0}", exception);
                }
                catch(Exception exception)
                {
                    _logger.LogCritical("Something went wrong when attempting to make an API call: {0}", exception);
                }

                //Error occured.
                return Enumerable.Empty<Ad>();
            }
        }

        public async Task<IEnumerable<Ad>> FindProductsByTitleAsync(string title)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    //Get URL from settings.
                    var url = _apiOptions.Value.Url;
                    client.BaseAddress = new Uri(url);

                    //Get request.
                    var response = await client.GetAsync($"/api/products/search/{ title }");

                    //Ensure that status code is successful.
                    response.EnsureSuccessStatusCode();

                    //Get Json string.
                    var responseString = await response.Content.ReadAsStringAsync();

                    //Deserialize Json string.
                    return JsonConvert.DeserializeObject<IEnumerable<Ad>>(responseString);
                }
                catch (HttpRequestException exception)
                {
                    _logger.LogCritical("API call resulted in an error: {0}", exception);
                }
                catch (Exception exception)
                {
                    _logger.LogCritical("Something went wrong when attempting to make an API call: {0}", exception);
                }

                //Error occured.
                return Enumerable.Empty<Ad>();
            }
        }

        public async Task<Ad> GetProductByIdAsync(Guid id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    //Get URL from settings.
                    var url = _apiOptions.Value.Url;
                    client.BaseAddress = new Uri(url);

                    //Get request.
                    var response = await client.GetAsync($"/api/products/{ id }");

                    //Ensure that status code is successful.
                    response.EnsureSuccessStatusCode();

                    //Get Json string.
                    var responseString = await response.Content.ReadAsStringAsync();

                    //Deserialize Json string.
                    return JsonConvert.DeserializeObject<Ad>(responseString);
                }
                catch (HttpRequestException exception)
                {
                    _logger.LogCritical("API call resulted in an error: {0}", exception);
                }
                catch (Exception exception)
                {
                    _logger.LogCritical("Something went wrong when attempting to make an API call: {0}", exception);
                }

                //Error occured.
                return null;
            }
        }
    }
}
