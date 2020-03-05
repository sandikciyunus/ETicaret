using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class ProductAddModel
    {
      
        public Product Product;
        public List<Category> Categories;
    }
}
