using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Refit;
using WebUI.Models;
using WebUI.Services;

namespace WebUI.Controllers
{
   
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class AdminController : Controller
    {
        private IConfiguration _configuration;
        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> AddProduct()
        {
            var categoryListApi = RestService.For<ICategoryApi>(_configuration.GetSection("MyAddress").Value);
            var categoryList = await categoryListApi.GetAll();
            var model = new ProductAddModel
            {
                Product = new Product(),
                Categories = categoryList
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product, IFormFile file)
        {

            if (file != null)
            {
                product.ImageUrl = file.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            var productAddApi = RestService.For<IAdminApi>(_configuration.GetSection("MyAddress").Value);
            if (ModelState.IsValid)
            {
                await productAddApi.AddProduct(product);
            }
            return RedirectToAction("GetAll", "Product");
        }
        public async Task<IActionResult> Index()
        {
            var productListApi = RestService.For<IProductApi>(_configuration.GetSection("MyAddress").Value);
            var productList = await productListApi.GetAll();
            return View(new ProductListViewModel()
            {
                Products = productList
            });
        }

        public async Task<IActionResult> Categories()
        {
            var categoryListApi = RestService.For<ICategoryApi>(_configuration.GetSection("MyAddress").Value);
            var categoryList = await categoryListApi.GetAll();
            return View(new CategoryListViewModel()
            {
                Categories = categoryList
            });
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            var categoryAddApi = RestService.For<IAdminApi>(_configuration.GetSection("MyAddress").Value);
            if (ModelState.IsValid)
            {
                await categoryAddApi.AddCategory(category);
            }
            return RedirectToAction("Categories", "Admin");
        }

        public async Task<IActionResult> UpdateCategory(int id)
        {
            var categoryUpdateApi = RestService.For<ICategoryApi>(_configuration.GetSection("MyAddress").Value);
            var categoryUpdate = await categoryUpdateApi.Get(id);
            return View(new CategoryUpdateModel()
            {
                Category = categoryUpdate
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            var categoryUpdateApi = RestService.For<IAdminApi>(_configuration.GetSection("MyAddress").Value);
            if (ModelState.IsValid)
            {
                await categoryUpdateApi.UpdateCategory(category.Id, category);
            }
            return RedirectToAction("Categories", "Admin");
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryDeleteApi = RestService.For<IAdminApi>(_configuration.GetSection("MyAddress").Value);
            var response = await categoryDeleteApi.DeleteCategory(id);
            return Json(new
            {
                isDeleted = response.Data,
                message = response.Data ? "Kayıt Silindi" : "Silinemedi"
            });
        }

        public async Task<IActionResult> UpdateProduct(int id)
        {
            var categoryListApi = RestService.For<ICategoryApi>(_configuration.GetSection("MyAddress").Value);
            var categoryList = await categoryListApi.GetAll();
            var productUpdateApi = RestService.For<IProductApi>(_configuration.GetSection("MyAddress").Value);
            var productUpdate = await productUpdateApi.Get(id);

            return View(new ProductUpdateModel()
            {
                Product = productUpdate,
                Categories = categoryList
            });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product, IFormFile file)
        {
            if (file != null)
            {
                product.ImageUrl = !string.IsNullOrEmpty(file.FileName) ? file.FileName : product.ImageUrl;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            var categoryListApi = RestService.For<ICategoryApi>(_configuration.GetSection("MyAddress").Value);
            var categoryList = await categoryListApi.GetAll();
            var productUpdateApi = RestService.For<IAdminApi>(_configuration.GetSection("MyAddress").Value);

            if (ModelState.IsValid)
            {
                await productUpdateApi.UpdateProduct(product.Id, product);
            }

            return View(new ProductUpdateModel()
            {
                Product = product,
                Categories = categoryList
            });
        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productDeleteApi = RestService.For<IAdminApi>(_configuration.GetSection("MyAddress").Value);
            await productDeleteApi.DeleteProduct(id);
            return Json(new
            {
                isDeleted = true,
                message = "Ürün başarıyla silindi"
            });
        }
    }
}