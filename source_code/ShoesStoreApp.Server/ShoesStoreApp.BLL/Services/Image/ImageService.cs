using Microsoft.AspNetCore.Http;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;
using Microsoft.EntityFrameworkCore;


namespace ShoesStoreApp.BLL.Services.Image
{
    public class ImageService:IImageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImageService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ImageSystem> SaveImageToDatabaseAsync(string fileName, string fileExtension, string urlPath)
        {
            var existingImage = await _unitOfWork.GenericRepository<ImageSystem>()
            .GetQuery(img => img.FileName == fileName && img.FileExtension == fileExtension)
            .FirstOrDefaultAsync();

            if (existingImage != null)
            {
                return existingImage;
            }

            var newImage = new ImageSystem
            {
                FileExtension = fileExtension,
                FileName = fileName,
                Url = urlPath,
                CreateDate = DateTime.Now,
            };

            _unitOfWork.GenericRepository<ImageSystem>().Add(newImage);
            await _unitOfWork.SaveChangesAsync();

            return newImage;
        }

        public void ValidateFileUpload(IFormFile file)
        {
            var allowExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                throw new ArgumentException("Unsupported file format");
            }

            if (file.Length > 10485760)
            {
                throw new ArgumentException("File size cannot be more than 10MB");
            }
        }
    }
}
