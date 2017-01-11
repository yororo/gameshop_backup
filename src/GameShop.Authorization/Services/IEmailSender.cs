using System.Threading.Tasks;

namespace GameShop.Authorization.Services {
    public interface IEmailSender {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
