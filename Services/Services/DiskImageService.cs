using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Services.Services.Interfaces;

namespace Services.Services
{
    public class DiskImageService : IImageService
    {
        public DiskImageService(string webRootPath)
        {
            this.webRootPath = webRootPath;
        }

        private readonly string webRootPath;
        private string imagesFolderName = "Images";

        public async Task<string> SaveImage(Type entityType, string imageName, IFormFile imageFile)
        {
            string imagesFolderPath = webRootPath + @$"\{imagesFolderName}";
            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            string entityFolderName = GetFolderNameByType(entityType);
            string entityFolderPath = imagesFolderPath + @$"\{entityFolderName}";

            if (!Directory.Exists(entityFolderPath))
            {
                Directory.CreateDirectory(entityFolderPath);
            }

            string imageFileName = CreateFileName(imageName, imageFile);
            string entityImagePath = entityFolderPath + @$"\{imageFileName}";

            using var imageFileStream = new FileStream(entityImagePath, FileMode.OpenOrCreate);

            await imageFile.CopyToAsync(imageFileStream);

            return @$"{imagesFolderName}/{entityFolderName}/{imageFileName}";
        }

        private static string GetFolderNameByType(Type entityType)
        {
            return entityType.Name;
        }

        private static string CreateFileName(string imageName, IFormFile image)
        {
            string imageFileExtension = Path.GetExtension(image.FileName);
            return (imageName).Replace(' ', '-') + imageFileExtension;
        }

        public void DeleteImage(Type entityType, string imageName, IFormFile imageFile)
        {
            string fileName = CreateFileName(imageName, imageFile);
            string entityFolder = GetFolderNameByType(entityType);

            string filePath = @$"{webRootPath}\{entityFolder}\{fileName}";

            File.Delete(filePath);
        }
    }
}
