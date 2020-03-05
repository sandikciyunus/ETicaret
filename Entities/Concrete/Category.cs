using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
   public class Category:IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Kategori adı boş geçilemez")]
        public string Name { get; set; }

       

       
    }
}
