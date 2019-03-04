using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace izibongo.api.DAL.Contracts.IRepositoryBase
{
    public interface IRepositoryBase<T>
    {
        IEnumerable<T> FindAll();
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}