namespace GameShop.Web.Services.GameShop.Interfaces
{
    public interface IGameShopTokenServices
    {
         string GetAccessToken(string username, string password);
    }
}