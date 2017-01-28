using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using GameShop.Api.Contracts;
using GameShop.Api.Contracts.Constants;
using GameShop.Api.Contracts.Responses;
using GameShop.Contracts.Entities;
using GameShop.Contracts.Entities.Products;
using GameShop.Contracts.Enumerations;
using GameShop.Contracts.Serialization.Json;
using GameShop.Data.Contracts.Products;

namespace GameShop.Api.Controllers.Products
{
    [Route(ApiEndpoints.Games)]
    public class GameController : Controller
    {
        #region Declarations

        private readonly IGameRepository _gameRepository;

        #endregion Declarations

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="gameRepository">Game repository.</param>
        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        #endregion Constructors

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Guid? id, [FromQuery]string title, [FromQuery]GameGenre? genre)
        {
            // No queries. Get all games.
            if(HttpContext.Request.Query.Count == 0)
            {
                return await GetAllAsync();
            }

            if(id.HasValue)
            {
                return await GetByIdAsync(id.Value);
            }

            if(!string.IsNullOrEmpty(title))
            {
                return await GetByNameAsync(title);
            }

            if(genre.HasValue)
            {
                return await GetByGenreAsync(genre.Value);
            }
            
            return Error("Unable to get game/s.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]Game game)
        {
            int result = await _gameRepository.AddAsync(game);
            if(result > 0)
            {
                return CreatedAtAction(nameof(GameController.CreateAsync), game);
            }

            return Error("Unable to create game.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]Game game)
        {
            // Check if IDs are the same.
            if(id != game.Id)
            {
                return Error("The ID parameter does not match the ID of the object payload.");
            }

            int result = await _gameRepository.UpdateAsync(id, game);

            if(result > 0)
            {
                return Ok();
            }

            // No rows were affected in the DB, 
            // so we will assume that a game with the given ID does not exist.
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            int result = await _gameRepository.DeleteByIdAsync(id);

            if(result > 0)
            {
                return Ok();
            }
            
            // No rows were affected in the DB, 
            // so we will assume that a game with the given ID does not exist.
            return NotFound();
        }
        
        //Create an API client library to be used for easy integration with the API.

        private async Task<IActionResult> GetAllAsync()
        {
            var games = await _gameRepository.GetAllAsync();

            return Ok(SerializationUtility.SerializeToJson(games));
        }

        private async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var game = await _gameRepository.GetByIdAsync(id);

            if (game != null)
            {
                return Ok(SerializationUtility.SerializeToJson(game));
            }

            return NotFound();
        }

        private async Task<IActionResult> GetByNameAsync(string name)
        {
            var games = await _gameRepository.GetByNameAsync(name);

            return Ok(SerializationUtility.SerializeToJson(games));
        }

        private async Task<IActionResult> GetByGenreAsync(GameGenre genre)
        {
            var games = await _gameRepository.GetByGenreAsync(genre);

            return Ok(SerializationUtility.SerializeToJson(games));
        }

        #region Functions

        /// <summary>
        /// Respond with OK result containing an error object.
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns>OK result with error object.</returns>
        private IActionResult Error(string errorMessage)
        {
            return Ok(SerializationUtility.SerializeToJson(new ApiResponse(Result.Error, errorMessage)));
        }

        #endregion Functions
    }
}
