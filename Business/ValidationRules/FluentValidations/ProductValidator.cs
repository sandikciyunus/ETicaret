using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidations
{
  public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Ürün ismi boş geçilemez");
            RuleFor(p => p.Name).Length(5, 30).WithMessage("Ürün ismi en az 5 en fazla 30 karakter olabilir");
            RuleFor(p => p.Price).NotEmpty().WithMessage("Fiyat alanı boş geçilemez");
          
        }
    }
}
