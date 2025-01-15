using Microsoft.AspNetCore.Identity;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.Custumer;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<UserVM> getUserByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        return new UserVM()
        {
            Id = user.Id,
            FullName = user.FullName,
            Address = user.Address,
            Email = user.Email,
        };
    }
}