using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Contracts.Requests
{
    public class SearchGameRequest : ApiRequest
    {
        public GameGenre GameGenre { get; set; }
        public GamingPlatform GamingPlatform { get; set; }
        public Rating Rating { get; set; }
        public PricingInformation MinimumPrice { get; set; }
        public PricingInformation MaximumPrice { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
