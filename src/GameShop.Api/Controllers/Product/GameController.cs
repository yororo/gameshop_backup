using GameShop.Contracts.Entities;
using GameShop.Data.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Controllers
{
    [Route("products/games)")]
    public class GameController : Controller
    {
        #region Declarations

        private IGameRepository _gameRepository;

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
        public async Task<IActionResult> Get([FromQuery]Guid? id, [FromQuery]string name)
        {
            // No queries. Get all games.
            if(HttpContext.Request.Query.Count == 0)
            {
                return Ok(await _gameRepository.GetAllAsync());
            }

            if(id.HasValue)
            {
                Game game = await _gameRepository.GetByIdAsync(id.Value);

                if (game != null)
                {
                    return Ok(game);
                }
            }
            else if(!string.IsNullOrEmpty(name))
            {
                IEnumerable<Game> games = await _gameRepository.GetByNameAsync(name);
                
                if (games.Count() != 0)
                {
                    return Ok(games);
                }
            }
            
            return NotFound();
        }
    }
}
