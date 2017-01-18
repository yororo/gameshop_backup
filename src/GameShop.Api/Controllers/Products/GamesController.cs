using GameShop.Contracts.Entities;
using GameShop.Data.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Api.Contracts.Responses;
using GameShop.Api.Contracts;
using GameShop.Contracts.Enumerations;

namespace GameShop.Api.Controllers.Products
{
    [Route("games")]
    public class GamesController : Controller
    {
        #region Declarations

        private readonly IGameRepository _gameRepository;

        #endregion Declarations

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="gameRepository">Game repository.</param>
        public GamesController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        #endregion Constructors

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Guid? id, [FromQuery]string name, [FromQuery]GameGenre? genre)
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

            if(genre.HasValue)
            {
                return await GetByGenreAsync(genre.Value);
            }
            
            return Ok(new ApiResponse(Result.Error, "Unable to get game/s."));
        }

        private async Task<IActionResult> GetAllAsync()
        {
            var games = await _gameRepository.GetAllAsync();

            return Ok(games);
        }

        private async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var game = await _gameRepository.GetByIdAsync(id);

            if (game != null)
            {
                return Ok(game);
            }

            return NotFound();
        }

        private async Task<IActionResult> GetByNameAsync(string name)
        {
            var games = await _gameRepository.GetByNameAsync(name);

            return Ok(games);
        }

        private async Task<IActionResult> GetByGenreAsync(GameGenre genre)
        {
            var games = await _gameRepository.GetByGenreAsync(genre);

            return Ok(games);
        }
    }
}
