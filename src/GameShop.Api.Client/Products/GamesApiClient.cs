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
using GameShop.Contracts.Enumerations;
using GameShop.Contracts.Serialization.Json;

namespace GameShop.Api.Client.Products
{
    public class GamesApiClient
    {
        private static HttpClient _client;

        public GamesApiClient(HttpClient client)
        {
            _client = client;
        }
        
        public async Task AddGameAsync(Game game, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                string json = SerializationUtility.SerializeToJson(game);

                var requestBody = new StringContent(json);
                requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await _client.PostAsync($"{ApiEndpoints.Games}",
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
                throw new ApiClientException("An error occured while attempting to add a game using the GameShop Api.", ex);
            }
        }

        public async Task UpdateGameAsync(Guid gameId, Game game, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                string json = SerializationUtility.SerializeToJson(game);

                var requestBody = new StringContent(json);
                requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await _client.PutAsync($"{ApiEndpoints.Games}?id={gameId}", 
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
                throw new ApiClientException("An error occured while attempting to update a game using the GameShop Api.", ex);
            }
        }

        public async Task DeleteGameAsync(Guid gameId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"{ApiEndpoints.Games}?id={gameId}",
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
                throw new ApiClientException("An error occured while attempting to update a game using the GameShop Api.", ex);
            }
        }

        public Task<List<Game>> GetAllGamesAsync()
        {
            return GetAsync<List<Game>>($"{ApiEndpoints.Games}");
        }
        
        public Task<Game> GetGameByIdAsync(Guid gameId)
        {
            return GetAsync<Game>($"{ApiEndpoints.Games}?id={gameId}");
        }

        public Task<List<Game>> GetGamesByTitleAsync(string title)
        {
            return GetAsync<List<Game>>($"{ApiEndpoints.Games}?title={title}");
        }

        public Task<List<Game>> GetGamesByGenreAsync(GameGenre genre)
        {
            return GetAsync<List<Game>>($"{ApiEndpoints.Games}?genre={genre}");
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