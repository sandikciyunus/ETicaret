using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Business.Messages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidations;
using Core.CrossCuttingConcerns.Validation;
using FluentValidation.Results;

namespace Business.Concrete
{
   public class ProductService:IProductService
    {
        private IProductDal _productDal;
       
        public ProductService(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [ValidationAspect(typeof(ProductValidator), Priority = 1)]
        public IResult Add(Product product)
        {
           
            _productDal.Add(product);
            return new SuccesResult(Messagess.ProductAdded);
            
        }

        public IResult Delete(int id)
        {
            var product = _productDal.Get(p => p.Id == id);
            _productDal.Delete(product);
            return new SuccesResult(Messagess.ProductDeleted);
           
        }

        public IDataResult<List<Product>> GetAll()
        {
            return new SuccesDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccesDataResult<Product>(_productDal.Get(p=>p.Id==productId));
        }

        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return  new SuccesDataResult<List<Product>>( _productDal.GetList(p => p.CategoryId == categoryId).ToList());
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccesResult(Messagess.ProductUpdated);
           
        }
    }
}
