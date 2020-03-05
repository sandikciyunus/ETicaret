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
    [Route("api/products")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private IProductService _productService;
      
        
        public ProductApiController(IProductService productService)
        {
            _productService = productService;
           
           
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _productService.GetAll();
            if(result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("category/{id}")]
        public  IActionResult GetListByCategory(int id)
        {
            var result = _productService.GetListByCategory(id);
            if(result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _productService.GetById(id);
            if(result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody]Product product)
        {

            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);


        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct([FromBody]Product product)
        {
            var result = _productService.Update(product);
            if(result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var result = _productService.Delete(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}