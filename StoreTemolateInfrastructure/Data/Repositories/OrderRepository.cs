using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using StoreTemplateCore.Entities;

namespace Infrastructure.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(StoreDbContext context) : base(context)
        {

        }
    }
}
