using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using izibongo.api.DAL.Contracts.IRepositoryBase;
using izibongo.api.DAL.DbContext;

namespace izibongo.api.DAL.Repository.RepositoryBase
{
    public class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        protected RepositoryContext _repositoryContext { get; set; }

        public void Create(T entity)
        {
           _repositoryContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _repositoryContext.Set<T>().Remove(entity);
        }

        public IEnumerable<T> FindAll()
        {
            return _repositoryContext.Set<T>();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _repositoryContext.Set<T>().Where(expression);
        }

        public void Save()
        {
            _repositoryContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _repositoryContext.Set<T>().Update(entity);
        }
    }
}