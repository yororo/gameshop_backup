using System.ComponentModel.DataAnnotations;

namespace GameShop.Authorization.ViewModels.Manage {
    public class AddPhoneNumberViewModel {
        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
