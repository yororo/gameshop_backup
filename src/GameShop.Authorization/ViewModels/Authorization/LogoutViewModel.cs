using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GameShop.Authorization.ViewModels.Authorization {
    public class LogoutViewModel {
        [BindNever]
        public string RequestId { get; set; }
    }
}
