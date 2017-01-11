using System.ComponentModel.DataAnnotations;

namespace GameShop.Authorization.ViewModels.Account {
    public class ForgotPasswordViewModel {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
