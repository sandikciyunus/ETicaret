using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetListByCategory(int categoryId);
        IDataResult<Product> GetById(int productId);
        IResult Add(Product product);
        IResult Delete(int id);

        IResult Update(Product product);
    }
}
