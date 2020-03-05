using Entities.Concrete;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Services
{
    public interface ICategoryApi
    {
        [Get("/api/categories")]
        Task<List<Category>> GetAll();

        [Get("/api/categories/{id}")]
        Task<Category> Get(int id);
    }
}
