
using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
  
    public class Product:IEntity
    {
        public int Id { get; set; }
     
       [Required(ErrorMessage ="Ürün ismi boş geçilemez")]
       [MinLength(5,ErrorMessage ="Minimum 5 karakter olmalı")]
       [MaxLength(30,ErrorMessage ="Maksimum 30 karakter olmalı")]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Fiyat alanı boş geçilemez")]
        public decimal Price { get; set; }

        [Required(ErrorMessage ="Açıklama alanı boş geçilemez")]
        public string Description { get; set; }

      


    }
}
