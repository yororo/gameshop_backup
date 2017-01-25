using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using GameShop.Api.Client.Exceptions;
using GameShop.Api.Contracts.Constants;
using GameShop.Contracts.Serialization.Json;
using GameShop.Contracts.Entities;
using GameShop.Contracts.Entities.Products;

namespace GameShop.Api.Client.Advertisements
{
    public class AdvertisementsApiClient
    {
        private static HttpClient _client;

        public AdvertisementsApiClient(HttpClient client)
        {
            _client = client;   
        }

        public async Task AddAdvertisementAsync(Advertisement advertisement, 
                            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                string json = SerializationUtility.SerializeToJson(advertisement);
                
                var requestBody = new StringContent(json);
                requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await _client.PostAsync($"{ApiEndpoints.Advertisements}", 
                                                                        requestBody, 
                                                                        cancellationToken);
                                                                        
                // Ensure that API responded with 200(OK) status.                                                        
                response.EnsureSuccessStatusCode();
            }
            catch(HttpRequestException ex)
            {
                throw new ApiClientException($"An error has occured while communicating with {_client.BaseAddress.ToString()}: {ex.Message}", ex);
            }
            catch(Exception ex)
            {
                throw new ApiClientException("An error occured while attempting to add an advertisement using the GameShop Api.", ex);
            }
        }

        public async Task UpdateAdvertisementAsync(Guid advertisementId, Advertisement advertisement, 
                            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                string json = SerializationUtility.SerializeToJson(advertisement);

                var requestBody = new StringContent(json);
                requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await _client.PutAsync($"{ApiEndpoints.Advertisements}?id={advertisementId}", 
                                                                        requestBody, 
                                                                        cancellationToken);

                // Ensure that API responded with 200(OK) status.                                                        
                response.EnsureSuccessStatusCode();
            }
            catch(HttpRequestException ex)
            {
                throw new ApiClientException($"An error has occured while communicating with {_client.BaseAddress.ToString()}: {ex.Message}", ex);
            }
            catch(Exception ex)
            {
                throw new ApiClientException("An error occured while attempting to update an advertisement using the GameShop Api.", ex);
            }
        }

        public async Task DeleteAdvertisementAsync(Guid advertisementId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"{ApiEndpoints.Advertisements}?id={advertisementId}",
                                                                        cancellationToken);

                // Ensure that API responded with 200(OK) status.                                                        
                response.EnsureSuccessStatusCode();
            }
            catch(HttpRequestException ex)
            {
                throw new ApiClientException($"An error has occured while communicating with {_client.BaseAddress.ToString()}: {ex.Message}", ex);
            }
            catch(Exception ex)
            {
                throw new ApiClientException("An error occured while attempting to delete an advertisement using the GameShop Api.", ex);
            }
        }

        public Task<List<Advertisement>> GetAllAdvertisementsAsync()
        {
            return GetAsync<List<Advertisement>>(ApiEndpoints.Advertisements);
        }

        public Task<List<Advertisement>> GetAllAdvertisementsDeepAsync()
        {
            return GetAsync<List<Advertisement>>($"{ApiEndpoints.Advertisements}?deep=1");
        }

        public Task<List<Product>> GetAdvertisementProducts(Guid advertisementId)
        {
            return GetAsync<List<Product>>(Path.Combine(ApiEndpoints.Advertisements, advertisementId.ToString(), "products"));
        }

        public Task<Advertisement> GetAdvertisementByIdAsync(Guid advertisementId)
        {
            return GetAsync<Advertisement>($"{ApiEndpoints.Advertisements}?id={advertisementId}");
        }

        public Task<Advertisement> GetAdvertisementByFriendlyIdAsync(string friendlyId)
        {
            return GetAsync<Advertisement>($"{ApiEndpoints.Advertisements}?fid={friendlyId}");
        }

        public Task<List<Advertisement>> GetAdvertisementsByTitleAsync(string title)
        {
            return GetAsync<List<Advertisement>>($"{ApiEndpoints.Advertisements}?title={title}");
        }


        private async Task<TResponse> GetAsync<TResponse>(string requestUrl)
        {
            try
            {
                string json = await _client.GetStringAsync(requestUrl);

                return SerializationUtility.DeserializeJson<TResponse>(json);
            }  
            catch(HttpRequestException ex)
            {
                throw new ApiClientException($"An error has occured while communicating with {Path.Combine(_client.BaseAddress.ToString(), requestUrl)}: {ex.Message}", ex);
            }
            catch(Exception ex)
            {
                throw new ApiClientException("An error occured while attempting to retrieve a game console/s using the GameShop Api.", ex);
            }          
        }
    }
}