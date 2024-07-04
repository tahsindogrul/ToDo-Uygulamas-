using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDo.Data.Repository.Shared.Absract;
using ToDo.Models;

namespace ToDo.Data.Repository.Shared.Concrete
{
    public class Repository<T> : IRepository<T> where T : BaseModel, new()
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();

        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            Save();
            return entity;
        }

        public ICollection<T> AddRange(ICollection<T> entities)
        {
            _dbSet.AddRange(entities);
            Save(); 
            return entities;
        }

        public void Delete(int id)
        {
           var item= _dbSet.Find(id);
            _dbSet.Remove(item);
            Save();
            return;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefault(expression);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity).Context.SaveChanges();
            
            return entity;
        }
    }
}
