using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Foundation.Business.Repositories
{
    public interface IBaseRepository <T> where T : class
    {
        Task<T> GetById(object id);
        Task<T> Add(T entity);
        Task Add(IEnumerable<T> entities);
        Task<T> Update(T entity);
        void Delete(object id);
        Task<T> GetByWhereAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate);
    }
}
