using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Refit;
using WebUI.Models;
using WebUI.Services;
using WebUI.SessionService;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private IConfiguration _configuration;
        private ICartSessionService _cartSessionService;
        private ICartService _cartService;
        public CartController(IConfiguration configuration,ICartSessionService cartSessionService,ICartService cartService)
        {
            _configuration = configuration;
            _cartSessionService = cartSessionService;
            _cartService = cartService;
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var productApi = RestService.For<IProductApi>(_configuration.GetSection("MyAddress").Value);
            var productToBeAdded = await productApi.Get(id);
            var cart = _cartSessionService.GetCart();
            _cartService.AddToCart(cart, productToBeAdded);
            _cartSessionService.SetCart(cart);
            TempData.Add("message", string.Format("Your product {0} was succesfully added to cart!", productToBeAdded.Name));
            return RedirectToAction("GetAll","Product");
        }

        public IActionResult List()
        {
            var cart = _cartSessionService.GetCart();
            CartSummaryViewModel cartSummaryViewModel = new CartSummaryViewModel
            {
                Cart = cart
            };
            return View(cartSummaryViewModel);
        }

        public IActionResult Remove(int Id)
        {
            var cart = _cartSessionService.GetCart();
            _cartService.RemoveFromCart(cart, Id);
            _cartSessionService.SetCart(cart);
            return RedirectToAction("List");
        }

        public IActionResult Complete()
        {
            var shippingDetailsViewModel = new ShippingDetailsViewModel
            {
                shippingDetails = new ShippingDetails()
            };
            return View(shippingDetailsViewModel);
        }

        [HttpPost]
        public IActionResult Complete(ShippingDetails shippingDetails)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            TempData.Add("message", String.Format("Thank you {0},you order is in procces", shippingDetails.FirstName));

            return View();
        }
    }
}