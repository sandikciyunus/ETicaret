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
        [Get("/api/products/category/{id}")]
        Task<List<Product>> GetListByCategory(int id);

        [Get("/api/products")]
        Task<List<Product>> GetAll();

        [Get("/api/products/{id}")]
        Task<Product> Get(int id);

       

    }
}
