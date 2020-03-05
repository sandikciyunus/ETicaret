using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetAll();
        IDataResult<Category> Get(int id);
        IResult Add(Category category);
        IResult Delete(int id);
        IResult Update(Category category);
    }
}
