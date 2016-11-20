using GameShop.Web.Services.GameShop.Interfaces;
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

namespace GameShop.Web.Services.GameShop
{
    public class GameShopAdvertisementsApi : IGameShopAdvertisementsApi
    {
        private IOptions<GameShopApiOptions> _apiOptions;
        private ILogger _logger;

        public GameShopAdvertisementsApi(IOptions<GameShopApiOptions> apiOptions, ILoggerFactory loggerFactory)
        {
            _apiOptions = apiOptions;
            _logger = loggerFactory.CreateLogger<IGameShopAdvertisementsApi>();    
        }

        public async Task<IEnumerable<Advertisement>> GetAllAdsAsync()
        {
            var response = await submitGetRequestAsync<IEnumerable<Advertisement>>($"/ads");

            if (response != null)
            {
                return response;
            }

            return Enumerable.Empty<Advertisement>();
        }

       public async Task<Advertisement> GetAdByIdAsync(Guid id)
        {
            return await submitGetRequestAsync<Advertisement>($"/ads/id/{ id }");
        }

        public async Task<Advertisement> GetAdByFriendlyIdAsync(string friendlyId)
        {
            return await submitGetRequestAsync<Advertisement>($"/ads/fid/{ friendlyId }");
        }
        public async Task<IEnumerable<Advertisement>> FindAdsByTitleAsync(string title)
        {
            var response = await submitGetRequestAsync<IEnumerable<Advertisement>>($"/ads/title/{ title }");

            if(response != null)
            {
                return response;
            }

            return Enumerable.Empty<Advertisement>();
        }

        /// <summary>
        /// Submits Http Get request.
        /// </summary>
        /// <typeparam name="TResponse">Expected type of response.</typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        private async Task<TResponse> submitGetRequestAsync<TResponse>(string path)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    //Get URL from settings.
                    var url = _apiOptions.Value.Url;
                    client.BaseAddress = new Uri(url);

                    //Get request.
                    var response = await client.GetAsync(path);

                    //Ensure that status code is successful.
                    response.EnsureSuccessStatusCode();

                    //Get Json string.
                    var responseString = await response.Content.ReadAsStringAsync();

                    //Deserialize Json string.
                    return JsonConvert.DeserializeObject<TResponse>(responseString);
                }
                catch (HttpRequestException exception)
                {
                    _logger.LogCritical("API call resulted in an error: {0}", exception);
                }
                catch (Exception exception)
                {
                    _logger.LogCritical("Something went wrong when attempting to make an API call: {0}", exception);
                }

                //Erro occured.
                return default(TResponse);
            }
        }
    }
}
