using Microsoft.AspNetCore.Http;
using ShoesStoreApp.DAL.Models;


namespace ShoesStoreApp.BLL.Services.Image
{
    public interface IImageService
    {
        Task<ImageSystem> SaveImageToDatabaseAsync(string fileName, string fileExtension);
        void ValidateFileUpload(IFormFile file);
    }
}
