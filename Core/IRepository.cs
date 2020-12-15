using System;
using System.Collections.Generic;

namespace ContactReport.Core
{
    public interface IRepository<T> where T : class
    {
        ApiResult Create(T entity);
        ApiResult Delete(Guid id);
        ApiResult<T> Get(Guid id);
        ApiResult<List<T>> GetAll();
    }
}
