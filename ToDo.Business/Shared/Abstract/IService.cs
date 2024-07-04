using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.Business.Shared.Abstract
{
    public interface IService<T> where T : BaseModel ,new()
    {
        T GetById(int id);
        T Add(T entity);
        ICollection<T> AddRange(ICollection<T> entities);

        T Update(T entity);
        bool Delete(int id);

        IQueryable<T> GetAll();

        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);

        T GetFirstOrDefault(Expression<Func<T, bool>> expression);

      
    }
}
