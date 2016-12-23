using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Web.Options
{
    public class GameShopAuthorizationOptions
    {
        /// <summary>
        /// URL of the GameShop authorization server.
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        /// GameShop API client ID.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// GameShop API client secret.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// The URL where the user agent will be returned to after application is signed out from the identity provider.
        /// </summary>
        public string PostLogoutRedirectUrl { get; set; }
    }
}
