using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Services;

namespace WebUI.ViewComponents
{
    public class CategoryListViewComponent:ViewComponent
    {
        private IConfiguration _configuration;
        public CategoryListViewComponent(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<ViewViewComponentResult> InvokeAsync()
        { 
         var categoryListApi = RestService.For<ICategoryApi>(_configuration.GetSection("MyAddress").Value);
        var categoryList = await categoryListApi.GetAll();
            var model = new CategoryListViewModel
            {
                Categories = categoryList,
                CurrentCategory = Convert.ToInt32(HttpContext.Request.Query["category"])
            };
            return View(model);
        }
       
        }
    }
