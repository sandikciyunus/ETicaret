using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        
        public ProductController(IProductService productService)
        {
            _productService = productService;
           
        }
        
        [HttpGet("getall")]
        public  IActionResult GetAll()
        {
            var result = _productService.GetAll();
            if(result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        //[HttpPost("add")]
        //public ActionResult Add(Product product)
        //{
        //    //var result = _productService.Add(product);
        //    //if(result.Success)
        //    //{
        //    //    return Ok(result.Message);
        //    //}

        //    //return BadRequest(result.Message);
        //}


        [HttpGet("get")]
        public IActionResult Get(int id)
        {
            var result = _productService.GetById(id);
            if(result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }  
    }
}