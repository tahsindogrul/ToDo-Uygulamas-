
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDo.Business.Shared.Abstract;
using ToDo.Data.Repository.Shared.Absract;
using ToDo.Models;

namespace ToDo.Business.Shared.Concrete
{
    public class Service<T> : IService<T> where T : BaseModel, new()
    {
        private readonly IRepository<T> _repo;

        public Service(IRepository<T> repo)
        {
            _repo = repo;
        }

        public T Add(T entity)
        {
            return _repo.Add(entity);
           
            
        }

        public ICollection<T> AddRange(ICollection<T> entities)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            _repo.Delete(id);
            return true;
        }

        public IQueryable<T> GetAll()
        {
            return _repo.GetAll();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _repo.GetAll(expression);
        }

        public T GetById(int id)
        {
           return _repo.GetById(id);
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return _repo.GetFirstOrDefault(expression);
        }

        public T Update(T entity)
        {
            return _repo.Update(entity);
        }
    }
}
