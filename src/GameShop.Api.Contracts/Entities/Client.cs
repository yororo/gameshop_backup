using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Contracts.Entities
{
    public class Client
    {
        #region Properties

        public Guid UniqueId { get; set; }
        public string ClientId { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public string RedirectUri { get; set; }
        public string LogoutUri { get; set; }
        public string ClientSecret { get; set; }
        public List<string> AllowedScopes { get; set; }

        #endregion Properties
    }
}
