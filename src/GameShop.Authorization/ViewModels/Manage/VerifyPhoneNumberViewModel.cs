using System.ComponentModel.DataAnnotations;

namespace GameShop.Authorization.ViewModels.Manage {
    public class VerifyPhoneNumberViewModel {
        [Required]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
