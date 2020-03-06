using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.Concrete
{
   public class Cart
    {
        public int Id { get; set; }
        public Cart()
        {
            CartLines = new List<CartLine>();
        }
        public List<CartLine> CartLines { get; set; }

        public decimal Total
        {
            get
            {
                return CartLines.Sum(p => p.Product.Price * p.Quantity);
            }
        }
    }
}
