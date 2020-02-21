using Entities.Concrete;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Services
{
    public interface IProductApi
    {
        [Get("/api/product/getall")]
        Task<List<Product>> GetAll();

        [Get("/api/product/get")]
        Task<Product> Get(int id);
    }
}
