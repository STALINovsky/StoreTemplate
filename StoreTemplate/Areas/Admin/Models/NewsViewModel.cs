using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using StoreTemplateCore.Entities;

namespace StoreTemplate.Areas.Admin.Models
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }

        public IFormFile Image { get; set; }

        public static NewsViewModel CreateByNews(News news)
        {
            return new NewsViewModel()
            {
                Id = news.Id,
                Name = news.Name,
                Description = news.Description,
                Text = news.Text
            };
        }
    }
}
