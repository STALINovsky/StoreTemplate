using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Services.Services.Interfaces
{
    public interface IImageService
    {
        public Task<string> SaveImage(Type entityType, string imageName, IFormFile imageFile);
        public void DeleteImage(Type entityType, string imageName, IFormFile imageFile);
    }
}
