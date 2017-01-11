using System.ComponentModel.DataAnnotations;

namespace GameShop.Authorization.ViewModels.Account {
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
