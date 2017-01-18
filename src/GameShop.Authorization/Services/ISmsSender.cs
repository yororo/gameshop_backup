using System.Threading.Tasks;

namespace GameShop.Authorization.Services {
    public interface ISmsSender {
        Task SendSmsAsync(string number, string message);
    }
}
