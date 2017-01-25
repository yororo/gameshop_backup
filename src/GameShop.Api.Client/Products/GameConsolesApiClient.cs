using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using GameShop.Api.Client.Exceptions;
using GameShop.Api.Contracts.Constants;
using GameShop.Contracts.Entities;
using GameShop.Contracts.Entities.Products;
using GameShop.Contracts.Serialization.Json;

namespace GameShop.Api.Client.Products
{
    public class GameConsolesApiClient
    {
        private static HttpClient _client;

        public GameConsolesApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task AddGameConsoleAsync(GameConsole gameConsole, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                string json = SerializationUtility.SerializeToJson(gameConsole);

                var requestBody = new StringContent(json);
                requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await _client.PostAsync($"{ApiEndpoints.GameConsoles}",
                                                                        requestBody,
                                                                        cancellationToken);
                // Ensure that API responded with 200(OK) status.                                                        
                response.EnsureSuccessStatusCode();
            }
            catch(HttpRequestException ex)
            {
                throw new ApiClientException($"An error has occured while communication with {_client.BaseAddress.ToString()}: {ex.Message}", ex);
            }
            catch(Exception ex)
            {
                throw new ApiClientException("An error occured while attempting to add a game console using the GameShop Api.", ex);
            }
        }

        public async Task UpdateGameConsoleAsync(Guid gameConsoleId, GameConsole gameConsole, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                string json = SerializationUtility.SerializeToJson(gameConsole);

                var requestBody = new StringContent(json);
                requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await _client.PutAsync($"{ApiEndpoints.GameConsoles}?id={gameConsoleId}", 
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
                throw new ApiClientException("An error occured while attempting to update a game console using the GameShop Api.", ex);
            }
        }

        public async Task DeleteGameConsoleAsync(Guid gameConsoleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"{ApiEndpoints.GameConsoles}?id={gameConsoleId}",
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
                throw new ApiClientException("An error occured while attempting to delete a game console using the GameShop Api.", ex);
            }
        }

        public Task<List<GameConsole>> GetAllGameConsolesAsync()
        {
            return GetAsync<List<GameConsole>>($"{ApiEndpoints.GameConsoles}");
        }
        
        public Task<GameConsole> GetGameConsoleByIdAsync(Guid gameConsoleId)
        {
            return GetAsync<GameConsole>($"{ApiEndpoints.GameConsoles}?id={gameConsoleId}");                    
        }


        public Task<List<GameConsole>> GetGameConsolesByPlatformAsync(string platform)
        {
            return GetAsync<List<GameConsole>>($"{ApiEndpoints.GameConsoles}?platform={platform}");
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