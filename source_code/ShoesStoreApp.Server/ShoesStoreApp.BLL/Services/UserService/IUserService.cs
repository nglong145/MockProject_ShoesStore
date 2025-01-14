using ShoesStoreApp.BLL.ViewModels;

namespace ShoesStoreApp.BLL.Services.Custumer;

public interface IUserService
{
    Task<UserVM> getUserByEmail(string email);
}