using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using GameShop.Api.Contracts.Constants;
using GameShop.Api.Contracts;
using GameShop.Api.Contracts.Responses;
using GameShop.Contracts.Entities;
using GameShop.Contracts.Entities.Products;
using GameShop.Contracts.Serialization.Json;
using GameShop.Data.Contracts.Products;

namespace GameShop.Api.Controllers.Products
{
    [Route(ApiEndpoints.GameConsoles)]
    public class GameConsoleController : Controller
    {
        #region Declarations

        private readonly IGameConsoleRepository _gameConsoleRepository;

        #endregion Declarations

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="gameRepository">Game repository.</param>
        public GameConsoleController(IGameConsoleRepository gameRepository)
        {
            _gameConsoleRepository = gameRepository;
        }

        #endregion Constructors

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Guid? id, [FromQuery]string name, [FromQuery]string platform)
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

            if(!string.IsNullOrEmpty(name))
            {
                return await GetByNameAsync(name);
            }

            if(!string.IsNullOrEmpty(platform))
            {
                return await GetByPlatformAsync(platform);
            }
            
            return Error("Unable to get game/s.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]GameConsole game)
        {
            int result = await _gameConsoleRepository.AddAsync(game);
            if(result > 0)
            {
                return CreatedAtAction(nameof(GameConsoleController.CreateAsync), game);
            }

            return Error("Unable to create game console.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]GameConsole gameConsole)
        {
            // Check if IDs are the same.
            if(id != gameConsole.Id)
            {
                return Error("The ID parameter does not match the ID of the object payload.");
            }

            int result = await _gameConsoleRepository.UpdateAsync(id, gameConsole);

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
            int result = await _gameConsoleRepository.DeleteByIdAsync(id);

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
            var games = await _gameConsoleRepository.GetAllAsync();

            return Ok(SerializationUtility.SerializeToJson(games));
        }

        private async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var game = await _gameConsoleRepository.GetByIdAsync(id);

            if (game != null)
            {
                return Ok(SerializationUtility.SerializeToJson(game));
            }

            return NotFound();
        }

        private async Task<IActionResult> GetByNameAsync(string name)
        {
            var games = await _gameConsoleRepository.GetByNameAsync(name);

            return Ok(SerializationUtility.SerializeToJson(games));
        }

        private async Task<IActionResult> GetByPlatformAsync(string platform)
        {
            var games = await _gameConsoleRepository.GetByPlatformAsync(platform);

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