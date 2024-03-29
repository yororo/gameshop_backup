﻿// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using GameShop.Api.Contracts.Requests;
// using GameShop.Data.Contracts;

// // For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

// namespace GameShop.Api.Controllers
// {
//     [Route("[controller]")]
//     public class SearchController : Controller
//     {
//         private IGameAdvertisementRepository _adRepository;

//         public SearchController(IGameAdvertisementRepository adRepository)
//         {
//             _adRepository = adRepository;
//         }

//         [HttpPost("ads")]
//         public IActionResult SearchGameAds([FromBody]SearchGameRequest searchAdRequest)
//         {
//             return Ok();
//         }

//         [HttpGet("ads/games/id/{id}")]
//         public IActionResult SearchAdById(Guid id)
//         {
//             return RedirectToRoute(new { controller = nameof(GameAdvertisementsController).Replace(nameof(Controller), string.Empty),
//                                             action = nameof(GameAdvertisementsController.FindGameAdvertisementByIdAsync),
//                                             @id = id });
//         }

//         [HttpGet("ads/games/fid/{friendlyId}")]
//         public IActionResult SearchAdByFriendlyId(string friendlyId)
//         {
//             return RedirectToRoute(new { controller = nameof(GameAdvertisementsController).Replace(nameof(Controller), string.Empty),
//                                             action = nameof(GameAdvertisementsController.FindGameAdvertisementByFriendlyIdAsync),
//                                             @friendlyId = friendlyId });
//         }

//         [HttpGet("ads/games/title/{title}")]
//         public IActionResult SearchAdByTitle(string title)
//         {
//             return RedirectToRoute(new { controller = nameof(GameAdvertisementsController).Replace(nameof(Controller), string.Empty),
//                                             action = nameof(GameAdvertisementsController.FindGameAdvertisementByTitleAsync),
//                                             @title = title });
//         }
//     }
// }
