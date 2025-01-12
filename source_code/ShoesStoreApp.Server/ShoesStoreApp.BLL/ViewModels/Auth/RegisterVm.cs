using System.ComponentModel.DataAnnotations;

namespace ShoesStoreApp.BLL.ViewModels.Auth
{
    public class RegisterVm
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
