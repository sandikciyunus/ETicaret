using Business.Abstract;
using Business.Messages;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private ICategoryDal _categoryDal;
        private IProductDal _productDal;
        public CategoryService(ICategoryDal categoryDal, IProductDal productDal)
        {
            _categoryDal = categoryDal;
            _productDal = productDal;
        }

        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccesResult(Messagess.CategoryAdded);
        }

        public IResult Delete(int id)
        {
            try
            {
                var category = _categoryDal.Get(p => p.Id == id);
                var product = _productDal.Get(p => p.CategoryId == category.Id);
                if (product != null)
                {
                    return new ErrorResult(Messagess.CategoryNotDeleted);
                }
                _categoryDal.Delete(category);
                return new SuccesResult(Messagess.CategoryDeleted);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        public IDataResult<Category> Get(int id)
        {
            return new SuccesDataResult<Category>(_categoryDal.Get(p => p.Id == id));
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccesDataResult<List<Category>>(_categoryDal.GetList().ToList());
        }

        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccesResult(Messagess.CategoryUpdated);
        }
    }
}
