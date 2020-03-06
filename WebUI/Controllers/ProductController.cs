using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Refit;
using WebUI.Models;
using WebUI.Services;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
       
        private IConfiguration _configuration;
        public ProductController(IConfiguration configuration)
        {
           
            _configuration = configuration;
          
        }
        public async Task<IActionResult> Index(int id)
        {
          
            var productListApi = RestService.For<IProductApi>(_configuration.GetSection("MyAddress").Value);
            var productList =  await productListApi.GetListByCategory(id);
            return View(new ProductListViewModel()
            {
                Products=productList
            });
        }

        public async Task<IActionResult> GetAll()
        {

            var productListApi = RestService.For<IProductApi>(_configuration.GetSection("MyAddress").Value);
            var productList = await productListApi.GetAll();
            return View(new ProductListViewModel()
            {
                Products = productList
            });
        }

        public async Task<IActionResult> Details(int id)
        {
            var productApi = RestService.For<IProductApi>(_configuration.GetSection("MyAddress").Value);
            var productDetails = await productApi.Get(id);
            return View(new ProductDetailsModel()
            {
                Product=productDetails
            });
        }
        
        
    }
}