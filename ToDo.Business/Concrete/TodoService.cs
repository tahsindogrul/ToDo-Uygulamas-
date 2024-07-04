using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Business.Shared.Concrete;
using ToDo.Data.Repository.Shared.Absract;
using ToDo.Models;

namespace ToDo.Business.Concrete
{
    public class TodoService:Service<Todo>,ITodoService
    {
        private readonly ITagService _tagService;

      

        public TodoService(IRepository<Todo> repo, ITagService tagService) :base(repo)
        {
            _tagService= tagService;
        }

        public Todo Add(Todo todo, int[] tagsIds, int userId)
        {
            todo.Tags = _tagService.GetAll(t => tagsIds.Contains(t.Id)).ToList();
            todo.AppUserId = userId;
            return Add(todo);
        }

        public IQueryable<Todo> GetAll(int userId)
        {
            return GetAll(t => t.AppUserId == userId).Select(t => new Todo
            {
                Id = t.Id,
                Name = t.Name,
                Status = t.Status,
                Description = t.Description,
                Tags = t.Tags.ToList()
            });
        }

        public bool RemoveTag(int todoId, int tagId)
        {
            Todo todo = GetAll(t => t.Id == todoId).Include(t => t.Tags).First();
            Tag tag = _tagService.GetById(tagId);
            todo.Tags.Remove(tag);
            Update(todo);
            return true;
        }
    }
}
