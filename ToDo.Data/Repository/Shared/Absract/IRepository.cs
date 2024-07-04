using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.Data.Repository.Shared.Absract
{
    public interface IRepository<T> where T : BaseModel,new()
    {
        T GetById(int id);
        T Add(T entity);
        ICollection<T> AddRange(ICollection<T> entities);

        T Update(T entity);
        void Delete(int id);

        IQueryable<T> GetAll();

        IQueryable<T> GetAll(Expression<Func<T,bool>>expression);

        T GetFirstOrDefault(Expression<Func<T,bool>>expression);

        void Save();


    }
}
