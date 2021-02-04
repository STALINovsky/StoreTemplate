using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Specifications.Base;
using StoreTemplateCore.Entities;

namespace Infrastructure.Specifications.NewsRepository
{
    public class NewsSpecification : Specification<News>
    {
        public NewsSpecification()
        {
            
        }

        public NewsSpecification(int id): base(news => news.Id == id )
        {

        }

        public NewsSpecification OrderByDate()
        {
            OrderByDescendingExpressions.Add(news => news.DateTime);
            return this;
        }
    }

    



}
