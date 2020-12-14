
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Services.Model;

namespace Services.Services.Interfaces
{
    public interface ICartService
    {
        Task<Cart> GetCartAsync();
    }
}
