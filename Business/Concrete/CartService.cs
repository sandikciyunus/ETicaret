using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CartService : ICartService
    {
        public void AddToCart(Cart cart, Product product)
        {
            CartLine cartLine = cart.CartLines.FirstOrDefault(p => p.Product.Id == product.Id);
            if (cartLine != null)
            {
                cartLine.Quantity++;
                return;
            }
            cart.CartLines.Add(new CartLine { Product = product, Quantity = 1 });
        }
        public List<CartLine> List(Cart cart)
        {
            return cart.CartLines;
        }
        public void RemoveFromCart(Cart cart, int Id)
        {
            CartLine cartLine = cart.CartLines.FirstOrDefault(p => p.Product.Id == Id);
            if (cartLine.Product.Name != null)
            {
                cartLine.Quantity--;
                if (cartLine.Quantity == 0)
                {
                    cart.CartLines.Remove(cartLine);
                }
                return;
            }
        }
    }
}
