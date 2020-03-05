using Core.Utilities.Results;
using Domain.Models;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Services
{
    public interface IAdminApi
    {
        [Post("/api/products")]
        Task AddProduct([Body] Product product);

        [Put("/api/products/{id}")]
        Task UpdateProduct(int id,[Body]Product product);

        [Delete("/api/products/{id}")]
        Task DeleteProduct(int id);

        [Post("/api/categories")]
        Task AddCategory([Body] Category category);

        [Put("/api/categories/{id}")]
        Task UpdateCategory(int id, [Body]Category category);

        [Delete("/api/categories/{id}")]
        Task<ApiResult<bool>>  DeleteCategory(int id);
    }
}
