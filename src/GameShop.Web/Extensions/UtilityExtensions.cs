using Microsoft.AspNetCore.Http;

namespace GameShop.Web
{
    public static class UtilityExtensions
    {
        private const string AccessTokenKey = "access_token";

        /// <summary>
        /// Get acces token from the http cookies.
        /// </summary>
        /// <param name="context">HttpContext.</param>
        /// <returns>Access token.</returns>
        public static string GetAccessToken(this HttpContext context)
        {
            return context.Request.Cookies[AccessTokenKey];
        }

        public static void StoreAccessToken(this HttpContext context, string accessToken)
        {
            context.Response.Cookies.Append(AccessTokenKey, accessToken);
        }
    }
}